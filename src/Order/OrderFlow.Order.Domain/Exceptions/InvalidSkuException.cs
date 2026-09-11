using OrderFlow.Order.Domain.Exceptions.Abstractions;

namespace OrderFlow.Order.Domain.Exceptions;

public sealed class InvalidSkuException : DomainException
{
    private new const string Message = "Invalid sku!";

    public InvalidSkuException()
        : base(Message)
    {
    }

    public InvalidSkuException(string message)
        : base(message)
    {
    }

    public InvalidSkuException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}