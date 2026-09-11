using OrderFlow.Order.Application.Dtos;

namespace OrderFlow.Order.Application.Abstractions.Services;

public interface IOrderService
{
    public Task CreateAsync(string customerId, IReadOnlyCollection<OrderLineDto> dtos);
}