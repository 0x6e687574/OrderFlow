using OrderFlow.Inventory.Domain.Exceptions.Abstractions;

namespace OrderFlow.Inventory.Domain.Exceptions;

public sealed class InvalidQuantityException : DomainException
{
    private new const string Message = "Invalid quantity!";

    public InvalidQuantityException()
        : base(Message)
    {
    }

    public InvalidQuantityException(string message)
        : base(message)
    {
    }

    public InvalidQuantityException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}