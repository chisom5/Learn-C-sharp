using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolarisEcl.Application.Common.Dtos;
using PolarisEcl.Application.Common.Interfaces;
using PolarisEcl.Domain.Exceptions;

namespace PolarisEcl.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnalysisController : BaseApiController
{
    private readonly IEclAnalysisService _analysisService;

    public AnalysisController(IEclAnalysisService service, IAppDbContext context) : base(context)
    {
        _analysisService = service;
    }

    [Authorize(Roles = "Staff")]
    [HttpPost]
    public async Task<IActionResult> createNewComputation([FromBody] AddNewComputationRequestDto request, [FromServices] IValidator<AddNewComputationRequestDto> validator)
    {
        var validatorResult = await validator.ValidateAsync(request);
        if (!validatorResult.IsValid)
        {
            throw new BadRequestException("", validatorResult.ToDictionary());
        }
        var user = await GetCurrentUserAsync();
        var result = await _analysisService.AddNewComputationAsync(request, user);

        return Ok(result);
    }

    [Authorize(Roles = "Staff")]
    [HttpPost("model_info")]
    public async Task<IActionResult> addModelInformation([FromBody] ModelInfoRequestDto request, [FromServices] IValidator<ModelInfoRequestDto> validator)
    {
        var validatorResult = await validator.ValidateAsync(request);
        if (!validatorResult.IsValid)
        {
            throw new BadRequestException("", validatorResult.ToDictionary());
        }

        var result = await _analysisService.SaveModelInformation(request);

        return Ok(result);
    }

}