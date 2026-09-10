using OrderFlow.Order.Domain.Exceptions.Abstractions;

namespace OrderFlow.Order.Domain.Exceptions;

public sealed class DuplicatedOrderLinesException : DomainException
{
    private new const string Message = "Duplicated order lines!";

    public DuplicatedOrderLinesException()
        : base(Message)
    {
    }

    public DuplicatedOrderLinesException(string message)
        : base(message)
    {
    }

    public DuplicatedOrderLinesException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}