using OrderFlow.Inventory.Domain.Exceptions.Abstractions;

namespace OrderFlow.Inventory.Domain.Exceptions;

public sealed class InvalidQuantityOnHandException : DomainException
{
    private new const string Message = "Invalid quantity on hand!";

    public InvalidQuantityOnHandException()
        : base(Message)
    {
    }

    public InvalidQuantityOnHandException(string message)
        : base(message)
    {
    }

    public InvalidQuantityOnHandException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}