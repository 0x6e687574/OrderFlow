using OrderFlow.Inventory.Domain.Exceptions.Abstractions;

namespace OrderFlow.Inventory.Domain.Exceptions;

public sealed class InvalidInventoryStatusException : DomainException
{
    private new const string Message = "Invalid inventory status!";

    public InvalidInventoryStatusException()
        : base(Message)
    {
    }

    public InvalidInventoryStatusException(string message)
        : base(message)
    {
    }

    public InvalidInventoryStatusException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}