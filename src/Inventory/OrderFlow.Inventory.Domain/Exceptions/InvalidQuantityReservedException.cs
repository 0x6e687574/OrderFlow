using OrderFlow.Inventory.Domain.Exceptions.Abstractions;

namespace OrderFlow.Inventory.Domain.Exceptions;

public sealed class InvalidQuantityReservedException : DomainException
{
    private new const string Message = "Invalid quantity reserved!";

    public InvalidQuantityReservedException()
        : base(Message)
    {
    }

    public InvalidQuantityReservedException(string message)
        : base(message)
    {
    }

    public InvalidQuantityReservedException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}