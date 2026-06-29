
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolarisEcl.Application.Common.Dtos;
using PolarisEcl.Application.Common.Interfaces;
using PolarisEcl.Domain.Exceptions;
using PolarisEcl.Filters;

namespace PolarisEcl.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin, Staff")]
[ServiceFilter(typeof(ActiveUserFilter))]
public class DashboardController : ControllerBase
{
    private readonly IDashboardSerivce _dashboardService;

    public DashboardController(IDashboardSerivce dashboardService)
    {
        _dashboardService = dashboardService;
    }

    
    [HttpGet("all")]
    public async Task<IActionResult> DashboardData([FromQuery] DashboardRequestDto request, IValidator<DashboardRequestDto> validator)
    {
         var validatorResult = await validator.ValidateAsync(request);

        if (!validatorResult.IsValid)
        {
            throw new BadRequestException("", validatorResult.ToDictionary());
        }

        DashboardResponseDto response = await _dashboardService.GetDashboardDataAsync(request);
        return Ok(response);

    }

    [HttpGet("distribution-by-segment")]
    public async Task<ActionResult> SegmentDistribution([FromQuery] DashboardRequestDto request, IValidator<DashboardRequestDto> validator)
    {
         var validatorResult = await validator.ValidateAsync(request);

        if (!validatorResult.IsValid)
        {
            throw new BadRequestException("", validatorResult.ToDictionary());
        }

        IEnumerable<SegmentDistributionDto> response = await _dashboardService.GetSegmentDistributionAsync(request);
        return Ok(response);
    }

    [HttpGet("distribution-by-stage")]
    public async Task<ActionResult> StageDistribution([FromQuery] DashboardRequestDto request, IValidator<DashboardRequestDto> validator)
    {
         var validatorResult = await validator.ValidateAsync(request);

        if (!validatorResult.IsValid)
        {
            throw new BadRequestException("", validatorResult.ToDictionary());
        }

        IEnumerable<StageDistributionDto> response = await _dashboardService.GetStageDistributionAsync(request);
        return Ok(response);
    }

    [HttpGet("ead-distribution")]
    public async Task<ActionResult> EadSegmentAndStage([FromQuery] DashboardRequestDto request, IValidator<DashboardRequestDto> validator)
    {
         var validatorResult = await validator.ValidateAsync(request);

        if (!validatorResult.IsValid)
        {
            throw new BadRequestException("", validatorResult.ToDictionary());
        }

        IEnumerable<EadByStageDto> response = await _dashboardService.GetEadBySegmentAndStageAsync(request);
        return Ok(response);
    }

    [HttpGet("trend-analysis")]
    public async Task<ActionResult> TrendChartByMonth([FromQuery] DashboardRequestDto request, IValidator<DashboardRequestDto> validator)
    {
         var validatorResult = await validator.ValidateAsync(request);

        if (!validatorResult.IsValid)
        {
            throw new BadRequestException("", validatorResult.ToDictionary());
        }

        IEnumerable<TrendDataDto> response = await _dashboardService.GetTrendChartByMonthAsync(request);
        return Ok(response);
    }

    [HttpGet("pd-distribution")]
    public async Task<ActionResult> PdDistributionBySegment([FromQuery] DashboardRequestDto request, IValidator<DashboardRequestDto> validator)
    {
         var validatorResult = await validator.ValidateAsync(request);

        if (!validatorResult.IsValid)
        {
            throw new BadRequestException("", validatorResult.ToDictionary());
        }

        IEnumerable<PDDistributionDto> response = await _dashboardService.GetPDDistributionBySegmentAsync(request);
        return Ok(response);
    }
}