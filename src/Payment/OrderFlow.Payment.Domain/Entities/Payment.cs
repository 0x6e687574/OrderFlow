using OrderFlow.Payment.Domain.Enums;
using OrderFlow.Payment.Domain.Exceptions;

namespace OrderFlow.Payment.Domain.Entities;

public sealed class Payment
{
    public Guid Id { get; private set; }
    public Guid OrderId { get; private set; }

    public decimal Amount
    {
        get;
        private set
        {
            if (value <= 0.0m)
            {
                throw new InvalidAmountException();
            }

            field = value;
        }
    }

    public PaymentStatus Status
    {
        get;
        private set
        {
            if (value is PaymentStatus.None)
            {
                throw new InvalidPaymentStatusException();
            }

            field = value;
        }
    }

    public DateTime CreatedAt { get; private set; }

    private Payment()
    {
    }

    public static Payment Create(Guid orderId, decimal amount, PaymentStatus status)
        => new()
        {
            Id = Guid.NewGuid(),
            OrderId = orderId,
            Amount = amount,
            Status = status,
            CreatedAt = DateTime.UtcNow
        };
}