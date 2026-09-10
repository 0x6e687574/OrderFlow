using OrderFlow.Order.Domain.Exceptions.Abstractions;

namespace OrderFlow.Order.Domain.Exceptions;

public sealed class InvalidOrderStatusException : DomainException
{
    private new const string Message = "Invalid order status!";

    public InvalidOrderStatusException()
        : base(Message)
    {
    }

    public InvalidOrderStatusException(string message)
        : base(message)
    {
    }

    public InvalidOrderStatusException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}