using Asp.Versioning;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolarisEcl.Application.Common.Dtos;
using PolarisEcl.Application.Common.Interfaces;
using PolarisEcl.Domain.Exceptions;

namespace PolarisEcl.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(Roles = "Staff")]
public class SettingsController : BaseApiController
{
    private readonly ISettingsService _settingsService;

    public SettingsController(ISettingsService settingsService, IAppDbContext context) : base(context)
    {
        _settingsService = settingsService;
    }

    [HttpGet]
    public async Task<IActionResult> FetchAllSettings()
    {
        var settings = await _settingsService.GetSettingsAsync();
        return Ok(settings);
    }

    [HttpPost("segment")]
    public async Task<IActionResult> CreateSegment([FromBody] SegmentRequestDto request, [FromServices] IValidator<SegmentRequestDto> validator)
    {
        var validatorResult = await validator.ValidateAsync(request);
        if (!validatorResult.IsValid)
        {
            throw new BadRequestException("", validatorResult.ToDictionary());
        }
        var user = await GetCurrentUserAsync();
        var result = await _settingsService.AddSegmentAsync(request, user);

        return Created(string.Empty, result);
    }

    [HttpPost("product")]
    public async Task<IActionResult> CreateProduct([FromBody] ProductRequestDto productRequestDto, [FromServices] IValidator<ProductRequestDto> validator)
    {
        var validatorResult = await validator.ValidateAsync(productRequestDto);
        if (!validatorResult.IsValid)
        {
            throw new BadRequestException("", validatorResult.ToDictionary());
        }
        var user = await GetCurrentUserAsync();
        var result = await _settingsService.AddProductAsync(productRequestDto, user);
       return Created(string.Empty, result);
    }


    [HttpPost("regression-params")]
    public async Task<IActionResult> CreateRegressionParams([FromBody] RegressionParamsRequestDto regressionParamsRequestDto, [FromServices] IValidator<RegressionParamsRequestDto> validator)
    {
        var validatorResult = await validator.ValidateAsync(regressionParamsRequestDto);
        if (!validatorResult.IsValid)
        {
            throw new BadRequestException("", validatorResult.ToDictionary());
        }
        var user = await GetCurrentUserAsync();
        var result = await _settingsService.AddRegressionParamsAsync(regressionParamsRequestDto, user);
       return Created(string.Empty, result);
    }

    [HttpPost("expected-correlation")]
    public async Task<IActionResult> CreateExpectedCorrelation([FromBody] ExpectedCorrelationRequestDto expectedCorrelationRequestDto, [FromServices] IValidator<ExpectedCorrelationRequestDto> validator)
    {
        var validatorResult = await validator.ValidateAsync(expectedCorrelationRequestDto);
        if (!validatorResult.IsValid)
        {
            throw new BadRequestException("", validatorResult.ToDictionary());
        }
        var user = await GetCurrentUserAsync();
        var result = await _settingsService.AddCorrelationAsync(expectedCorrelationRequestDto, user);
        return Created(string.Empty, result);
    }

    [HttpPost("collateral-type")]
    public async Task<IActionResult> CreateCollateralType([FromBody] CollateralTypeRequestDto collateralTypeRequestDto, [FromServices] IValidator<CollateralTypeRequestDto> validator)
    {
        var validatorResult = await validator.ValidateAsync(collateralTypeRequestDto);
        if (!validatorResult.IsValid)
        {
            throw new BadRequestException("", validatorResult.ToDictionary());
        }
        var user = await GetCurrentUserAsync();
        var result = await _settingsService.AddCollateralTypeAsync(collateralTypeRequestDto, user);
      return Created(string.Empty, result);
    }

    [HttpPut("segment/{segmentId}")]
    public async Task<IActionResult> UpdateSegment([FromBody] UpdateSegmentRequestDto request, [FromRoute] Guid segmentId, [FromServices] IValidator<UpdateSegmentRequestDto> validator)
    {
        var validatorResult = await validator.ValidateAsync(request);
        if (!validatorResult.IsValid)
        {
            throw new BadRequestException("", validatorResult.ToDictionary());
        }

        var result = await _settingsService.UpdateSegmentAsync(request, segmentId);
        return Ok(result);
    }

    [HttpPut("product/{productId}")]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductRequestDto request, [FromRoute] Guid productId, [FromServices] IValidator<UpdateProductRequestDto> validator)
    {
        var validatorResult = await validator.ValidateAsync(request);
        if (!validatorResult.IsValid)
        {
            throw new BadRequestException("", validatorResult.ToDictionary());
        }

        var result = await _settingsService.UpdateProductAsync(request, productId);
        return Ok(result);
    }

    [HttpPut("regression-params/{regressionParamId}")]
    public async Task<IActionResult> UpdateRegressionParams([FromBody] UpdateRegressionParamsRequestDto request, [FromRoute] Guid regressionParamId, [FromServices] IValidator<UpdateRegressionParamsRequestDto> validator)
    {
        var validatorResult = await validator.ValidateAsync(request);
        if (!validatorResult.IsValid)
        {
            throw new BadRequestException("", validatorResult.ToDictionary());
        }

        var result = await _settingsService.UpdateRegressionParamsAsync(request, regressionParamId);
        return Ok(result);
    }

    [HttpPut("expected-correlation/{correlationId}")]
    public async Task<IActionResult> UpdateCorrelation([FromBody] UpdateExpectedCorrelationRequestDto request, [FromRoute] Guid correlationId, [FromServices] IValidator<UpdateExpectedCorrelationRequestDto> validator)
    {
        var validatorResult = await validator.ValidateAsync(request);
        if (!validatorResult.IsValid)
        {
            throw new BadRequestException("", validatorResult.ToDictionary());
        }

        var result = await _settingsService.UpdateCorrelationAsync(request, correlationId);
        return Ok(result);
    }

    [HttpPut("collateral-type/{collateralTypeId}")]
    public async Task<IActionResult> UpdateCollateralType([FromBody] UpdateCollateralTypeRequestDto request, [FromRoute] Guid collateralTypeId, [FromServices] IValidator<UpdateCollateralTypeRequestDto> validator)
    {
        var validatorResult = await validator.ValidateAsync(request);
        if (!validatorResult.IsValid)
        {
            throw new BadRequestException("", validatorResult.ToDictionary());
        }

        var result = await _settingsService.UpdateCollateralTypeAsync(request, collateralTypeId);
        return Ok(result);
    }

    [HttpDelete("segment/{segmentId}")]
    public async Task<IActionResult> DeleteSegment([FromRoute] Guid segmentId)
    {
        if (segmentId == Guid.Empty)
        {
            throw new BadRequestException("Segment Id must be provided.");
        }
        await _settingsService.DeleteSegmentAsync(segmentId);
        return NoContent();
    }

    [HttpDelete("product/{productId}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] Guid productId)
    {
        if (productId == Guid.Empty)
        {
            throw new BadRequestException("Product Id must be provided.");
        }
        await _settingsService.DeleteProductAsync(productId);
        return NoContent();
    }

    [HttpDelete("regression-params/{regressionParamId}")]
    public async Task<IActionResult> DeleteRegressionParams([FromRoute] Guid regressionParamId)
    {
        if (regressionParamId == Guid.Empty)
        {
            throw new BadRequestException("Regression Parameter Id must be provided.");
        }
        await _settingsService.DeleteRegressionParamsAsync(regressionParamId);
        return NoContent();
    }

    [HttpDelete("expected-correlation/{correlationId}")]
    public async Task<IActionResult> DeleteCorrelation([FromRoute] Guid correlationId)
    {
        if (correlationId == Guid.Empty)
        {
            throw new BadRequestException("Expected Correlation Id must be provided.");
        }
        await _settingsService.DeleteCorrelationAsync(correlationId);
        return NoContent();
    }

    [HttpDelete("collateral-type/{collateralTypeId}")]
    public async Task<IActionResult> DeleteCollateralType([FromRoute] Guid collateralTypeId)
    {
        if (collateralTypeId == Guid.Empty)
        {
            throw new BadRequestException("Collateral Type Id must be provided.");
        }
        await _settingsService.DeleteCollateralTypeAsync(collateralTypeId);
        return NoContent();
    }
}