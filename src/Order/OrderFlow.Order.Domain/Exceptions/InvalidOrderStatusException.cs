using OrderFlow.Order.Domain.Exceptions.Abstractions;

namespace OrderFlow.Order.Domain.Exceptions;

public class InvalidOrderStatusException : DomainException
{
    public InvalidOrderStatusException()
    {
    }

    public InvalidOrderStatusException(string message) : base(message)
    {
    }

    public InvalidOrderStatusException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
