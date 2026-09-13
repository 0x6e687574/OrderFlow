namespace OrderFlow.Order.Application.Abstractions.Repositories;

using Order = Domain.Entities.Order;

public interface IOrderRepository
{
    public Task AddAsync(Order order);
    public Task<Order?> GetByIdAsync(Guid orderId);
    public Task<Order?> GetByCustomerIdAsync(string customerId);
}