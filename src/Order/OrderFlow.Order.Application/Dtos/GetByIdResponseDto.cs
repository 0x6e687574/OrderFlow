namespace OrderFlow.Order.Application.Dtos;

public class GetByIdResponseDto
{
    public Guid OrderId { get; set; }
    public string CustomerId { get; set; } = null!;
    public string Status { get; set; } = null!;
    public decimal TotalAmount { get; set; }
    public bool ReservationCompleted { get; set; }
    public bool PaymentCompleted { get; set; }
    public IReadOnlyCollection<GetByIdOrderLineResponseDto> OrderLines { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}