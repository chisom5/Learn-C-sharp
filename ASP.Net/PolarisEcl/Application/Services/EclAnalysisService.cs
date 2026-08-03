using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PolarisEcl.Application.Common.Dtos;
using PolarisEcl.Application.Common.Interfaces;
using PolarisEcl.Domain.Enums;
using PolarisEcl.Domain.Models;

namespace PolarisEcl.Application.Services;

/*
 - Add new compuation
 - save model info progress
 - start computation
 - view report
 - view results (3 results.)
 - get all computation

 - IEclAnalysisService
*/
public class EclAnalysisService : IEclAnalysisService
{
    private readonly IAppDbContext _context;
    private readonly ILogger<EclAnalysisService> _logger;


    public EclAnalysisService(IAppDbContext context, ILogger<EclAnalysisService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<NewComputationResponseDto> AddNewComputationAsync(AddNewComputationRequestDto request, User user)
    {
        if (!DateTime.TryParseExact(
            $"{request.Month} {request.Year}",
            ["MMMM yyyy", "MMM yyyy"],
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out DateTime parsedStartDate))
        {
            throw new ArgumentException("Invalid month or year provided");
        }
        var startDate = DateOnly.FromDateTime(parsedStartDate);
        var endDate = startDate.AddMonths(12).AddDays(-1);

        string startPeriodFormat = startDate.ToString("MMMM yyyy");
        string endPeriodFormat = endDate.ToString("MMM yyyy");

        var computationD = new ECLComputation
        {
            Id = Guid.NewGuid(),
            ComputationName = $"ECL computation for [{startPeriodFormat} - {endPeriodFormat}]",
            Status = ComputationStatus.NotDone,
            ReportingStartDate = startDate,
            ReportingEndDate = endDate,
            CreatedAt = DateTime.UtcNow,
            ComputedBy = user,
            ComputedById = user.Id
        };

        _context.ECLComputations.Add(computationD);
        await _context.SaveChangesAsync();

        _logger.LogInformation($"New computation successfully created for: {computationD.ComputationName}");
        return new NewComputationResponseDto
        {
            Id = computationD.Id,
            ComputationName = computationD.ComputationName
        };

    }

    public async Task<string> SaveModelInformation(ModelInfoRequestDto request)
    {
        var currentCompuation = await _context.ECLComputations.FirstOrDefaultAsync((ecl) => ecl.Id == request.ComputationId);

        if (currentCompuation == null)
        {
            throw new KeyNotFoundException($"ECL Computation with ID '{request.ComputationId}' was not found.");
        }

        currentCompuation.PdWeightBaseline = request.Pd_Baseline;
        currentCompuation.PdWeightBestcase = request.Pd_Bestcase;
        currentCompuation.PdWeightWorstcase = request.Pd_Worstcase;
        currentCompuation.HistoricalMargin = request.HistoricalMargin;
        currentCompuation.MacroeconomicAdjustmentFactor = request.AdjustmentFactor;

        await _context.SaveChangesAsync();
        
        _logger.LogInformation($"Model information updated for computation ID: {request.ComputationId}");
        return "Successful";
    }
}