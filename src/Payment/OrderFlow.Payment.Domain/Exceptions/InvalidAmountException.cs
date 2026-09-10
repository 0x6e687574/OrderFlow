using OrderFlow.Payment.Domain.Exceptions.Abstractions;

namespace OrderFlow.Payment.Domain.Exceptions;

public sealed class InvalidAmountException : DomainException
{
    private new const string Message = "Invalid amount!";

    public InvalidAmountException()
        : base(Message)
    {
    }

    public InvalidAmountException(string message)
        : base(message)
    {
    }

    public InvalidAmountException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}