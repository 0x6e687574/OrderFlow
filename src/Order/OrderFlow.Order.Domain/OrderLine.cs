namespace OrderFlow.Order.Domain;

public class OrderLine
{
    public Guid Id  { get; set; }
    public required string CustomerId { get; set; }
    public double TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}