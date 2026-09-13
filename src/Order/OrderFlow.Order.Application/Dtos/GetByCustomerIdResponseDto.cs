namespace OrderFlow.Order.Application.Dtos;

public class GetByCustomerIdResponseDto
{
    public Guid OrderId { get; set; }
    public string Status { get; set; } = null!;
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; }
}