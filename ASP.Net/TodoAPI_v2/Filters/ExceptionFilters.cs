using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TodoAPI.Filters;

// This filter handles exceptions that occur during the execution of controller actions,
// allowing for centralized error handling and consistent error responses across the API.

public class GlobalExceptionFilter : IExceptionFilter 
{
    public void OnException(ExceptionContext context)
    {
        string userFriendlyMessage;
        int statusCode;

        switch (context.Exception)
        {
            case ArgumentNullException:
                userFriendlyMessage = "Required data are missing. Please ensure all required fields are provided.";
                statusCode = 400;
                break;

            case UnauthorizedAccessException:
                userFriendlyMessage = "You are not authorized to perform this action.";
                statusCode = 403;
                break;

            case InvalidOperationException:
                userFriendlyMessage = "The operation could not be completed for this action, please try again later.";
                statusCode = 400;
                break;

            case NullReferenceException:
                userFriendlyMessage = "An unexpected error occurred while processing your request. Please try again later.";
                statusCode = 500;
                break;

            case Exception:
                userFriendlyMessage = "Something went wrong. Please try again later.";
                statusCode = 500;
                break;

            default:
                userFriendlyMessage = "An unknown error occurred.";
                statusCode = 500;
                break;
        }

        context.Result = new ViewResult
        {
            ViewName = "CustomError",
            StatusCode = statusCode,
            ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), context.ModelState)
            {
                ["Message"] = userFriendlyMessage,
            }
        };

        context.ExceptionHandled = true;
    }
}