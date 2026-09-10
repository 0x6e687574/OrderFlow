using OrderFlow.Order.Domain.Exceptions;

namespace OrderFlow.Order.Domain.Entities;

public sealed class OrderSagaState
{
    public Guid OrderId { get; private set; }
    public bool ReservationCompleted { get; private set; }
    public bool PaymentCompleted { get; private set; }
    public Guid? LastProcessedEventId { get; private set; }

    private OrderSagaState()
    {
    }

    public static OrderSagaState Create(Guid orderId)
        => new()
        {
            OrderId = orderId
        };

    public void Reserve(Guid eventId, bool reservationCompleted)
    {
        EnsureState(ReservationCompleted);

        ReservationCompleted = reservationCompleted;
        LastProcessedEventId = eventId;
    }

    public void Pay(Guid eventId, bool paymentCompleted)
    {
        EnsureState(PaymentCompleted);

        PaymentCompleted = paymentCompleted;
        LastProcessedEventId = eventId;
    }

    private static void EnsureState(bool state)
    {
        if (state is not false)
        {
            throw new InvalidSagaStateException();
        }
    }
}