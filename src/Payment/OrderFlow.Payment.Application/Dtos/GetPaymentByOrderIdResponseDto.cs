namespace OrderFlow.Payment.Application.Dtos;

public class GetPaymentByOrderIdResponseDto
{
    public Guid PaymentId { get; set; }
    public Guid OrderId { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}