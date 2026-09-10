using OrderFlow.Order.Domain.Exceptions.Abstractions;

namespace OrderFlow.Order.Domain.Exceptions;

public sealed class EmptyOrderLinesException : DomainException
{
    private new const string Message = "Empty order lines!";

    public EmptyOrderLinesException()
        : base(Message)
    {
    }

    public EmptyOrderLinesException(string message)
        : base(message)
    {
    }

    public EmptyOrderLinesException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}