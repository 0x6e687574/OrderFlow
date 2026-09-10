using OrderFlow.Payment.Domain.Exceptions.Abstractions;

namespace OrderFlow.Payment.Domain.Exceptions;

public sealed class InvalidPaymentStatusException : DomainException
{
    private new const string Message = "Invalid payment status!";

    public InvalidPaymentStatusException()
        : base(Message)
    {
    }

    public InvalidPaymentStatusException(string message)
        : base(message)
    {
    }

    public InvalidPaymentStatusException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}