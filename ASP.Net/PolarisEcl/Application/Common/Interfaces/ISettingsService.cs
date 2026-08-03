using PolarisEcl.Application.Common.Dtos;
using PolarisEcl.Domain.Models;

namespace PolarisEcl.Application.Common.Interfaces;

public interface ISettingsService
{
    Task<SettingsResponseDto> GetSettingsAsync();
    Task<SegmentResponseDto> AddSegmentAsync(SegmentRequestDto request, User user);
    Task<ProductResponseDto> AddProductAsync(ProductRequestDto request, User user);
    Task<RegressionParamsResponseDto> AddRegressionParamsAsync(RegressionParamsRequestDto request, User user);
    Task<ExpectedCorrelationResponseDto> AddCorrelationAsync(ExpectedCorrelationRequestDto request, User user);
    Task<CollateralTypeResponseDto> AddCollateralTypeAsync(CollateralTypeRequestDto request, User user);

    Task<SegmentResponseDto> UpdateSegmentAsync(UpdateSegmentRequestDto request, Guid segmentId);
    Task<ProductResponseDto> UpdateProductAsync(UpdateProductRequestDto request, Guid productId);
    Task<RegressionParamsResponseDto> UpdateRegressionParamsAsync(UpdateRegressionParamsRequestDto request, Guid regressionId);
    Task<ExpectedCorrelationResponseDto> UpdateCorrelationAsync(UpdateExpectedCorrelationRequestDto request, Guid correlationId);
    Task<CollateralTypeResponseDto> UpdateCollateralTypeAsync(UpdateCollateralTypeRequestDto request, Guid collateralId);

    Task<string> DeleteSegmentAsync(Guid segmentId);
    Task<string> DeleteProductAsync(Guid productId);
    Task<string> DeleteRegressionParamsAsync(Guid regressionId);
    Task<string> DeleteCorrelationAsync(Guid correlationId);
    Task<string> DeleteCollateralTypeAsync(Guid collateralTypeId);

}