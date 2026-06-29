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

        var baseQuery = _context.EclDataSnapshot.AsNoTracking().Where(d => d.Year == request.Year);

        if (request.Month.HasValue)
        {
            baseQuery = baseQuery.Where(d => d.Month == request.Month.Value);
        }

        var dataSnapshots = await baseQuery.ToListAsync();

        // handle empty database result.
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
                    ? await _context.EclDataSnapshot.AsNoTracking().Where(d => d.Year == request.Year).ToListAsync()
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
        var dataSnapshots = _context.EclDataSnapshot.AsNoTracking();

        dataSnapshots = dataSnapshots.Where(d => d.Year == request.Year);
        if (request.Month.HasValue)
        {
            dataSnapshots = dataSnapshots.Where(d => d.Month == request.Month);
        }

        var chart1Data = await dataSnapshots
                            .GroupBy(g => g.ProductType.ToString())
                            .Select(s => new SegmentDistributionDto
                            {
                                Segment = FormatSegmentName(s.Key),
                                ECL = s.Sum(x => x.ECL),
                                EAD = s.Sum(x => x.EAD)
                            }).ToListAsync();

        return chart1Data;
    }

    public async Task<IEnumerable<StageDistributionDto>> GetStageDistributionAsync(DashboardRequestDto request)
    {
        var dataSnapshots = _context.EclDataSnapshot.AsNoTracking();

        dataSnapshots = dataSnapshots.Where(d => d.Year == request.Year);
        if (request.Month.HasValue)
        {
            dataSnapshots = dataSnapshots.Where(d => d.Month == request.Month);
        }

        var chart1Data = await dataSnapshots
                            .GroupBy(g => g.Stage.ToString())
                            .Select(s => new StageDistributionDto
                            {
                                Stages = "Stage " + s.Key,
                                ECL = s.Sum(x => x.ECL),
                                EAD = s.Sum(x => x.EAD)
                            }).ToListAsync();

        return chart1Data;
    }

    public async Task<IEnumerable<EadByStageDto>> GetEadBySegmentAndStageAsync(DashboardRequestDto request)
    {
        var dataSnapshots = _context.EclDataSnapshot.AsNoTracking();

        dataSnapshots = dataSnapshots.Where(d => d.Year == request.Year);
        if (request.Month.HasValue)
        {
            dataSnapshots = dataSnapshots.Where(d => d.Month == request.Month);
        }

        var chart2Data = await dataSnapshots
                            .GroupBy(g => g.ProductType.ToString())
                          .Select(s => new EadByStageDto
                          {
                              Segment = FormatSegmentName(s.Key),
                              Stage1 = s.Where(v => v.Stage == ECLStage.Stage1).Sum(val => val.EAD),
                              Stage2 = s.Where(v => v.Stage == ECLStage.Stage2).Sum(val => val.EAD),
                              Stage3 = s.Where(v => v.Stage == ECLStage.Stage3).Sum(val => val.EAD)
                          }).ToListAsync();

        return chart2Data;
    }

    public async Task<IEnumerable<TrendDataDto>> GetTrendChartByMonthAsync(DashboardRequestDto request)
    {
        var dataSnapshots = _context.EclDataSnapshot.AsNoTracking().Where(d => d.Year == request.Year);

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
        var dataSnapshots = _context.EclDataSnapshot.AsNoTracking();

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