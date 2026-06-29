namespace PolarisEcl.Domain.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}
public class UnauthorizedException : Exception
{
    public UnauthorizedException(string message) : base(message) { }
}
public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message) { }
    public IDictionary<string, string[]>? Errors { get; }
    public BadRequestException(string message, IDictionary<string, string[]> errors) : base(message)
    {
        Errors = errors;
    }
}
