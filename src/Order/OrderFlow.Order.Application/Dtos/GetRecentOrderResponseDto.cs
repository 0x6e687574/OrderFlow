namespace OrderFlow.Order.Application.Dtos;

public class GetRecentOrderResponseDto
{
    public Guid OrderId { get; set; }
    public string CustomerId { get; set; } = null!;
    public string Status { get; set; } = null!;
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}