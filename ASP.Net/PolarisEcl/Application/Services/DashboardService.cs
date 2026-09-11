using Microsoft.EntityFrameworkCore;
using PolarisEcl.Application.Common.Dtos;
using PolarisEcl.Application.Common.Interfaces;
using PolarisEcl.Domain.Enums;
using PolarisEcl.Domain.Exceptions;

namespace PolarisEcl.Application.Services;

public class DashboardService : IDashboardSerivce
{
    private readonly IAppDbContext _context;

    public DashboardService(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardResponseDto> GetDashboardDataAsync(DashboardRequestDto request)
    {

        var baseQuery = _context.Loans.AsNoTracking().Where(d => d.Year == request.Year);

        if (request.Month.HasValue)
        {
            baseQuery = baseQuery.Where(d => d.Month == request.Month.Value);
        }

        var dataSnapshots = await baseQuery.ToListAsync();

        if (dataSnapshots.Count == 0)
        {
            throw new NotFoundException($"No financial data records found for Year {request.Year}{(request.Month.HasValue ? $", Month {request.Month}" : "")}.");
        }

        var chart1Data = dataSnapshots
                            .Where(d => d.Year == request.Year && d.Month == request.Month)
                            .GroupBy(g => g.ProductType.ToString())
                            .Select(s => new SegmentDistributionDto
                            {
                                Segment = FormatSegmentName(s.Key),
                                ECL = s.Sum(x => x.ECL),
                                EAD = s.Sum(x => x.EAD)
                            });

        var chart2Data = dataSnapshots
                            .Where(d => d.Year == request.Year && d.Month == request.Month)
                            .GroupBy(g => g.ProductType.ToString())
                            .Select(s => new EadByStageDto
                            {
                                Segment = FormatSegmentName(s.Key),
                                Stage1 = s.Where(v => v.Stage == ECLStage.Stage1).Sum(val => val.EAD),
                                Stage2 = s.Where(v => v.Stage == ECLStage.Stage2).Sum(val => val.EAD),
                                Stage3 = s.Where(v => v.Stage == ECLStage.Stage3).Sum(val => val.EAD)
                            });

        // get all the trend record of all the report that has been approve.
        // await _context.ECLReport.Include(s => s.ECLComputation).Where(d => d.ReportStatus == ReportReivew.Approved)
        // .OrderBy(r => r.ECLComputation.ReportingPeriod)
        // .Take(12)
        // .Select(s => new TrendDataDto
        // {
        //     Month = s.ECLComputation.ReportingPeriod.ToString("MMM"),
        //     ECL = s.TotalECL,
        //     EAD = s.TotalEAD
        // }).ToListAsync();

        var yearlyDataSnapshots = request.Month.HasValue
                    ? await _context.Loans.AsNoTracking().Where(d => d.Year == request.Year).ToListAsync()
                    : dataSnapshots;

        var chart3Data = yearlyDataSnapshots
                            .Where(d => d.Year == request.Year)
                            .GroupBy(g => g.Month)
                            .Select(s => new TrendDataDto
                            {
                                Month = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(s.Key),
                                ECL = s.Sum(x => x.ECL),
                                EAD = s.Sum(x => x.EAD)
                            }).OrderBy(g => g.Month);


        var chart4Data = dataSnapshots
                            .Where(d => d.Year == request.Year && d.Month == request.Month)
                            .GroupBy(g => g.ProductType.ToString())
                            .Select(s => new PDDistributionDto
                            {
                                Segment = FormatSegmentName(s.Key),
                                RG1 = s.Any() ? s.Average(x => x.PD) : 0,
                                RG2 = s.Any() ? s.Average(x => x.PD) : 0,
                            });

        return new DashboardResponseDto
        {
            DistributionBySegment = chart1Data,
            EadBySegmentAndStage = chart2Data,
            TrendChartByMonth = chart3Data,
            PDDistributionBySegment = chart4Data
        };
    }

    public async Task<IEnumerable<SegmentDistributionDto>> GetSegmentDistributionAsync(DashboardRequestDto request)
    {
        var dataSnapshots = _context.Loans.AsNoTracking();

        dataSnapshots = dataSnapshots.Where(d => d.Year == request.Year);
        if (request.Month.HasValue)
        {
            dataSnapshots = dataSnapshots.Where(d => d.Month == request.Month);
        }

        var rawData = await dataSnapshots
                            .GroupBy(g => g.ProductType)
                            .Select(s => new
                            {
                                ProductKey = s.Key,
                                ECL = s.Sum(x => x.ECL),
                                EAD = s.Sum(x => x.EAD)
                            }).ToListAsync();

        return rawData.Select(d => new SegmentDistributionDto
        {
            Segment = FormatSegmentName(d.ProductKey.ToString()),
            ECL = d.ECL,
            EAD = d.EAD
        });
    }

    public async Task<IEnumerable<StageDistributionDto>> GetStageDistributionAsync(DashboardRequestDto request)
    {
        var dataSnapshots = _context.Loans.AsNoTracking();

        dataSnapshots = dataSnapshots.Where(d => d.Year == request.Year);
        if (request.Month.HasValue)
        {
            dataSnapshots = dataSnapshots.Where(d => d.Month == request.Month);
        }

        var raw1Data = await dataSnapshots
                            .GroupBy(g => g.Stage)
                            .Select(s => new
                            {
                                Stages = s.Key,
                                ECL = s.Sum(x => x.ECL),
                                EAD = s.Sum(x => x.EAD)
                            }).ToListAsync();

        return raw1Data.Select(d => new StageDistributionDto
        {
            Stages = $"Stage {d.Stages}",
            ECL = d.ECL,
            EAD = d.EAD
        });
    }

    public async Task<IEnumerable<EadByStageDto>> GetEadBySegmentAndStageAsync(DashboardRequestDto request)
    {
        var dataSnapshots = _context.Loans.AsNoTracking();

        dataSnapshots = dataSnapshots.Where(d => d.Year == request.Year);
        if (request.Month.HasValue)
        {
            dataSnapshots = dataSnapshots.Where(d => d.Month == request.Month);
        }

        var chart2Data = await dataSnapshots
                            .GroupBy(g => g.ProductType)
                          .Select(s => new
                          {
                              Segment = s.Key,
                              Stage1 = s.Sum(val => val.Stage == ECLStage.Stage1 ? val.EAD : 0),
                              Stage2 = s.Sum(val => val.Stage == ECLStage.Stage2 ? val.EAD : 0),
                              Stage3 = s.Sum(val => val.Stage == ECLStage.Stage3 ? val.EAD : 0)
                          }).ToListAsync();

        return chart2Data.Select(d => new EadByStageDto
        {
            Segment = FormatSegmentName(d.Segment.ToString()),
            Stage1 = d.Stage1,
            Stage2 = d.Stage2,
            Stage3 = d.Stage3
        });
    }

    public async Task<IEnumerable<TrendDataDto>> GetTrendChartByMonthAsync(DashboardRequestDto request)
    {
        var dataSnapshots = _context.Loans.AsNoTracking().Where(d => d.Year == request.Year);

        var chart3Data = await dataSnapshots
                          .GroupBy(g => g.Month)
                            .Select(s => new TrendDataDto
                            {
                                Month = s.Key.ToString(),
                                ECL = s.Sum(x => x.ECL),
                                EAD = s.Sum(x => x.EAD)
                            }).OrderBy(g => g.Month).ToListAsync();

        foreach (var item in chart3Data)
        {
            if (int.TryParse(item.Month, out int mNum))
            {
                item.Month = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(mNum);
            }
        }

        return chart3Data;
    }

    public async Task<IEnumerable<PDDistributionDto>> GetPDDistributionBySegmentAsync(DashboardRequestDto request)
    {
        var dataSnapshots = _context.Loans.AsNoTracking();

        dataSnapshots = dataSnapshots.Where(d => d.Year == request.Year);
        if (request.Month.HasValue)
        {
            dataSnapshots = dataSnapshots.Where(d => d.Month == request.Month);
        }

        var chart4Data = await dataSnapshots
                           .GroupBy(g => g.ProductType.ToString())
                            .Select(s => new PDDistributionDto
                            {
                                Segment = FormatSegmentName(s.Key),
                                RG1 = s.Any() ? s.Average(x => x.PD) : 0,
                                RG2 = s.Any() ? s.Average(x => x.PD) : 0,
                            }).ToListAsync();

        return chart4Data;
    }

    private static string FormatSegmentName(string productType)
    {
        return productType switch
        {
            "Group1" => "Group 1",
            "GroupRegular1" => "GroupRegular 1",
            "GroupRegular2" => "GroupRegular 2",
            "GroupRegular3" => "GroupRegular 3",
            "GroupRegular4" => "GroupRegular 4",
            "GroupRegular5" => "GroupRegular 5",
            "Individual" => "Individual",
            "SME" => "SME",
            "Staff" => "Staff",
            _ => productType
        };
    }
}