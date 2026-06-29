using PolarisEcl.Application.Common.Dtos;

namespace PolarisEcl.Application.Common.Interfaces;

public interface IDashboardSerivce
{
    Task<DashboardResponseDto> GetDashboardDataAsync(DashboardRequestDto request);
    Task<IEnumerable<SegmentDistributionDto>> GetSegmentDistributionAsync(DashboardRequestDto request);
    Task<IEnumerable<StageDistributionDto>> GetStageDistributionAsync(DashboardRequestDto request);
    Task<IEnumerable<EadByStageDto>> GetEadBySegmentAndStageAsync(DashboardRequestDto request);
    Task<IEnumerable<TrendDataDto>> GetTrendChartByMonthAsync(DashboardRequestDto request);
    Task<IEnumerable<PDDistributionDto>> GetPDDistributionBySegmentAsync(DashboardRequestDto request);
}