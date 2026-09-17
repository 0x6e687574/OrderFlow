namespace OrderFlow.Inventory.Application.Events;

public class ReservationSucceededEvent
{
    public Guid OrderId { get; set; }
    public IReadOnlyCollection<ReservationSucceededOrderLineEvent> OrderLines { get; set; } = null!;
}