using OrderFlow.Order.Domain.Exceptions.Abstractions;

namespace OrderFlow.Order.Domain.Exceptions;

public sealed class InvalidSagaStateException : DomainException
{
    private new const string Message = "Invalid saga state!";

    public InvalidSagaStateException()
        : base(Message)
    {
    }

    public InvalidSagaStateException(string message)
        : base(message)
    {
    }

    public InvalidSagaStateException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}