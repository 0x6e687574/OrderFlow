using Microsoft.EntityFrameworkCore;
using OrderFlow.Order.Application.Abstractions.Repositories;

namespace OrderFlow.Order.Infrastructure.Persistence.Repositories;

using Order = Domain.Entities.Order;

public class OrderRepository(OrderDbContext orderDbContext) : IOrderRepository
{
    public async Task AddAsync(Order order)
        => await orderDbContext.Orders.AddAsync(order);

    public async Task<Order?> GetByIdAsync(Guid orderId)
        => await orderDbContext
            .Orders
            .AsNoTracking()
            .Include(o => o.OrderLines)
            .Include(o => o.OrderSagaState)
            .FirstOrDefaultAsync(o => o.Id == orderId);

    public async Task<IReadOnlyCollection<Order>> GetAllByCustomerIdAsync(string customerId)
        => await orderDbContext
            .Orders
            .AsNoTracking()
            .Where(o => o.CustomerId == customerId)
            .ToListAsync();
}