using OrderFlow.Client.Dtos.Order;

namespace OrderFlow.Client.Clients.Abstractions;

public interface IOrderClient
{
    public Task<CreateOrderResponseDto> CreateOrderAsync(CreateOrderRequestDto dto);
    public Task<GetOrderByIdDto> GetOrderByIdAsync(Guid orderId);
    public Task<IEnumerable<GetRecentOrderDto>> GetRecentOrdersAsync();
}