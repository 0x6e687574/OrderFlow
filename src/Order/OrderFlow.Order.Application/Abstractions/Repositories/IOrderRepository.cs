namespace OrderFlow.Order.Application.Abstractions.Repositories;

using Order = Domain.Entities.Order;

public interface IOrderRepository
{
    public Task AddAsync(Order order);
    public Task<Order?> FindAsync(Guid id);
    public Task<bool> ExistsAsync(Guid id);
}