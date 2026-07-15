using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolarisEcl.Application.Common.Interfaces;
using PolarisEcl.Domain.Enums;
using PolarisEcl.Domain.Exceptions;
using PolarisEcl.Filters;

namespace PolarisEcl.Controllers;


[ApiController]
[Route("api/[controller]")]
[ServiceFilter(typeof(ActiveUserFilter))]
public class TemplateController : ControllerBase
{
    private readonly ITemplateService _templateService;

    public TemplateController(ITemplateService service)
    {
        _templateService = service;
    }

    [Authorize(Roles = "Admin, Staff")]
    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadTemplate([FromForm] UploadTemplateRequestDto request, [FromServices] IValidator<UploadTemplateRequestDto> validator)
    {

        var validatorResult = await validator.ValidateAsync(request);

        if (!validatorResult.IsValid)
        {
            throw new BadRequestException("", validatorResult.ToDictionary());
        }

        var currentUserIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub");
        if (!Guid.TryParse(currentUserIdStr, out var userId))
        {
            return Unauthorized("Unable to resolve valid user session token.");
        }

        var response = await _templateService.UploadTemplateAsync(request, userId);

        return Ok(new { message = response });
    }

    [Authorize(Roles = "Admin, Staff")]
    [HttpGet("defaults")]
    public async Task<IActionResult> GetDefaultTemplates()
    {
        var response = await _templateService.GetDefaultTemplatesAsync();

        return Ok(response);

    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("default")]
    public async Task<IActionResult> DeleteDefaultTemplate(Guid templateId)
    {
        if (templateId == Guid.Empty)
        {
            throw new BadRequestException("A valid templateId is required.");
        }

        var response = await _templateService.DeleteDefaultTemplateAsync(templateId);

        return Ok(new { message = response });

    }

    [Authorize(Roles = "Admin, Staff")]
    [HttpGet("download")]
    public async Task<IActionResult> DownloadDefaultTemplate(FileType fileType)
    {
        if (!Enum.IsDefined(typeof(FileType), fileType))
        {
            throw new BadRequestException("The specified template file type option is invalid.");
        }
        var response = await _templateService.DownloadDefaultTemplateAsync(fileType);

        return File(response.FileStream, response.ContentType, response.FileName);
    }

}