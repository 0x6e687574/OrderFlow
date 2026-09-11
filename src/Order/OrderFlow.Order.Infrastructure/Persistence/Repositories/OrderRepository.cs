using Microsoft.EntityFrameworkCore;
using OrderFlow.Order.Application.Abstractions.Repositories;

namespace OrderFlow.Order.Infrastructure.Persistence.Repositories;

using Order = Domain.Entities.Order;

public class OrderRepository(OrderDbContext orderDbContext) : IOrderRepository
{
    public async Task AddAsync(Order order)
        => await orderDbContext.Orders.AddAsync(order);

    public async Task<Order?> FindAsync(Guid id)
        => await orderDbContext.Orders.FindAsync(id);

    public async Task<bool> ExistsAsync(Guid id)
        => await orderDbContext.Orders.AnyAsync(o => o.Id == id);
}