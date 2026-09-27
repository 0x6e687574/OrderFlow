namespace OrderFlow.Client.Dtos.Order;

public class CreateOrderRequestDto
{
    public string CustomerId { get; set; } = null!;
    public List<CreateOrderLineRequestDto> OrderLines { get; } = [new()];
}