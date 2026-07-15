
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PolarisEcl.Application.Common.Wrappers;

namespace PolarisEcl.Filters;

public class ResponseWrapperFilter : ActionFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.Result is ObjectResult objectResult && objectResult.Value is not null)
        {
            if (objectResult.Value.GetType().IsGenericType &&
            objectResult.Value.GetType().GetGenericTypeDefinition() == typeof(ApiResponse<>))
            {
                return;
            }

            var statusCode = objectResult.StatusCode ?? 200;

            var wrappedResponse = ApiResponse<object>.Success(
                data: objectResult.Value,
                message: "Success",
                statusCode: statusCode
            );

            objectResult.Value = wrappedResponse;
        }
    }
}