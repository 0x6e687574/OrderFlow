using OrderFlow.Order.Application.Abstractions.Repositories;

namespace OrderFlow.Order.Infrastructure.Persistence.Repositories;

using Order = Domain.Entities.Order;

public class OrderRepository(OrderDbContext orderDbContext) : IOrderRepository
{
    public async Task AddAsync(Order order)
        => await orderDbContext.Orders.AddAsync(order);
}