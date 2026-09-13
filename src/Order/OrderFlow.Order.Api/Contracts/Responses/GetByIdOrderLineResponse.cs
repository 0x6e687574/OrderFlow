namespace OrderFlow.Order.Api.Contracts.Responses;

public class GetByIdOrderLineResponse
{
    public string Sku { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}