namespace OrderFlow.Order.Api.Contracts.Responses;

public class GetByCustomerIdResponse
{
    public Guid OrderId { get; set; }
    public string Status { get; set; } = null!;
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; }
}