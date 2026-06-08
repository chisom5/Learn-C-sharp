using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Filters;
using TodoAPI.Models;

namespace TodoAPI.Filters;

// This filter validates the payload for POST and PATCH requests 
// to ensure that it adheres to the defined data annotations and business rules before reaching the controller action.
public class ValidateTodoPayloadFilter : IAsyncActionFilter
{

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var request = context.HttpContext.Request;

        var errorMessages = new Dictionary<string, List<string>>();

        if (request.Method == HttpMethods.Post || request.Method == HttpMethods.Patch)
        {
            var payload = context.ActionArguments.Values.FirstOrDefault(x => x is Todos);

            if (payload is null)
            {
                errorMessages["Payload"] = ["Request body is required."];
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.HttpContext.Response.WriteAsJsonAsync(new { Errors = errorMessages });

                return;

            }


            // business logic validation for 
            // DueDate can not be in the past, 
            // then when the task should not be completed for a post request.
            var todo = payload as Todos;

            // Post rules
            if (request.Method == HttpMethods.Post)
            {
                if (todo is not null && todo.IsCompleted)
                {
                    if (!errorMessages.TryGetValue("IsCompleted", out List<string>? value))
                    {
                        errorMessages["IsCompleted"] = ["A new task cannot be marked as completed."];
                    }

                    else
                    {
                        var existingErrors = value.ToList();
                        existingErrors.Add("A new task cannot be marked as completed.");
                        errorMessages["IsCompleted"] = existingErrors;
                    }
                }
                if (todo is not null && todo.DueDate.Date < DateTime.UtcNow.Date)
                {
                    if (!errorMessages.TryGetValue("DueDate", out List<string>? value))
                    {
                        errorMessages["DueDate"] = ["Due date cannot be in the past."];
                    }
                    else
                    {
                        var existingErrors = value.ToList();
                        existingErrors.Add("Due date cannot be in the past.");
                        errorMessages["DueDate"] = existingErrors;
                    }
                }
            }

            // Patch rules
            if (request.Method == HttpMethods.Patch)
            {
                if (todo is not null && todo.DueDate != default &&
                    todo.DueDate.Date < DateTime.UtcNow.Date)
                {
                    errorMessages["DueDate"] = ["Due date cannot be in the past."];

                }

                if (todo is not null && todo.Name is not null && string.IsNullOrWhiteSpace(todo.Name))
                {
                    errorMessages["Name"] = ["Name cannot be empty."];

                }
            }


            // data annotations validation
            var validationContext = new ValidationContext(payload);
            var validationResults = new List<ValidationResult>();

            Validator.TryValidateObject(
              payload,
              validationContext,
              validationResults,
              validateAllProperties: true);

            foreach (var result in validationResults)
            {
                foreach (var memberName in result.MemberNames)
                {
                    if (!errorMessages.TryGetValue(memberName, out List<string>? value))
                    {
                        errorMessages[memberName] = [result.ErrorMessage ?? "Invalid value."];
                    }
                    else
                    {
                        var existingErrors = value.ToList();
                        existingErrors.Add(result.ErrorMessage ?? "Invalid value.");
                        errorMessages[memberName] = existingErrors;
                    }

                }
            }


            // If there are any validation errors, return a 400 Bad Request response with the error details.
            if (errorMessages.Count > 0)
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.HttpContext.Response.WriteAsJsonAsync(new { Errors = errorMessages });
                return;
            }

        }

        await next();
    }

}

