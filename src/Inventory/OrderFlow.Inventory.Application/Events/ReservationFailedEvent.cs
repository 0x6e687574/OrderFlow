namespace OrderFlow.Inventory.Application.Events;

public class ReservationFailedEvent
{
    public Guid OrderId { get; set; }
    public string Reason { get; set; } = null!;
}