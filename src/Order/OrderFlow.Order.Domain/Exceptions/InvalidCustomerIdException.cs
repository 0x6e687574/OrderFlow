using OrderFlow.Order.Domain.Exceptions.Abstractions;

namespace OrderFlow.Order.Domain.Exceptions;

public sealed class InvalidCustomerIdException : DomainException
{
    private new const string Message = "Invalid customer id!";

    public InvalidCustomerIdException()
        : base(Message)
    {
    }

    public InvalidCustomerIdException(string message)
        : base(message)
    {
    }

    public InvalidCustomerIdException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}