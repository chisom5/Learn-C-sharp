using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PolarisEcl.Domain.Exceptions;

namespace PolarisEcl.Infrastructure.Errors;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly IProblemDetailsService __problemDetailsService;

    public GlobalExceptionHandler(IProblemDetailsService problemDetailsService)
    {
        __problemDetailsService = problemDetailsService;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        string title = "Internal Server error";
        int statusCode = StatusCodes.Status500InternalServerError;
        IDictionary<string, string[]>? validationErrors = null; //fluent validation

        switch (exception)
        {
            case NotFoundException:
            case KeyNotFoundException:
                statusCode = StatusCodes.Status404NotFound;
                title = "Resource Not Found";
                break;

            case UnauthorizedException:
            case UnauthorizedAccessException:
                statusCode = StatusCodes.Status401Unauthorized;
                title = "Unauthorized Access";
                break;

            case BadRequestException badRequestEx:
                {
                    statusCode = StatusCodes.Status400BadRequest;
                    title = "Bad Request";
                    validationErrors = badRequestEx.Errors;
                    break;
                }

            case ArgumentException:
            case InvalidOperationException:
                statusCode = StatusCodes.Status400BadRequest;
                title = "Bad Request";
                break;

            default:
                // If it's a generic Exception thrown by your app logic
                statusCode = StatusCodes.Status500InternalServerError;
                title = "An unexpected error occurred";
                break;
        }

        httpContext.Response.StatusCode = statusCode;
        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = exception.Message,
            Instance = httpContext.Request.Path
        };

        if (validationErrors != null)
        {
            problemDetails.Extensions["errors"] = validationErrors;
        }

        return await __problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = problemDetails
        });
    }
}