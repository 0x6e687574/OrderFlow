namespace OrderFlow.Order.Domain.Entities;

public class OrderSagaState
{
    public Guid OrderId { get; private set; }
    public bool ReservationCompleted { get; private set; }
    public bool PaymentCompleted { get; private set; }
    public Guid? LastProcessedEventId { get; private set; }

    public Order Order { get; private set; } = null!;
}