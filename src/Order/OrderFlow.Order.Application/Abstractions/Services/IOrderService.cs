using OrderFlow.Order.Application.Dtos;

namespace OrderFlow.Order.Application.Abstractions.Services;

public interface IOrderService
{
    public Task CreateAsync(OrderDto dto);
}