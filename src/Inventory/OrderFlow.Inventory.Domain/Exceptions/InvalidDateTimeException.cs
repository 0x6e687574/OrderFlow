using OrderFlow.Inventory.Domain.Exceptions.Abstractions;

namespace OrderFlow.Inventory.Domain.Exceptions;

public sealed class InvalidDateTimeException : DomainException
{
    private new const string Message = "Invalid datetime!";

    public InvalidDateTimeException()
        : base(Message)
    {
    }

    public InvalidDateTimeException(string message)
        : base(message)
    {
    }

    public InvalidDateTimeException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}