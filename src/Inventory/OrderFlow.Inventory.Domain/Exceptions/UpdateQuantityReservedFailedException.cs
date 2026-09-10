using OrderFlow.Inventory.Domain.Exceptions.Abstractions;

namespace OrderFlow.Inventory.Domain.Exceptions;

public sealed class UpdateQuantityReservedFailedException : DomainException
{
    private new const string Message = "Failed to update quantity reserved!";

    public UpdateQuantityReservedFailedException()
        : base(Message)
    {
    }

    public UpdateQuantityReservedFailedException(string message)
        : base(message)
    {
    }

    public UpdateQuantityReservedFailedException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}