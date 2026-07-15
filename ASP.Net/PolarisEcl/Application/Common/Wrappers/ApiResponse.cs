namespace PolarisEcl.Application.Common.Wrappers;

public class ApiResponse<T>
{
    public string Message { get; set; } = string.Empty;
    public string StatusCode { get; set; } = string.Empty;
    public bool Status { get; set; }
    public T? Data { get; set; }

    public static ApiResponse<T> Success(T data, string message = "Success", int statusCode = 200) =>
        new() { Status = true, StatusCode = statusCode.ToString(), Message = message, Data = data };
}