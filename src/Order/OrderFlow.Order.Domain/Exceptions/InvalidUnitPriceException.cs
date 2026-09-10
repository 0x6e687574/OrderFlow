using OrderFlow.Order.Domain.Exceptions.Abstractions;

namespace OrderFlow.Order.Domain.Exceptions;

public sealed class InvalidUnitPriceException : DomainException
{
    private new const string Message = "Invalid unit price!";

    public InvalidUnitPriceException()
        : base(Message)
    {
    }

    public InvalidUnitPriceException(string message)
        : base(message)
    {
    }

    public InvalidUnitPriceException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}