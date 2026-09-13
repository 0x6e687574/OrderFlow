using OrderFlow.Order.Application.Abstractions.Repositories;
using OrderFlow.Order.Application.Abstractions.UnitOfWorks;

namespace OrderFlow.Order.Infrastructure.Persistence.UnitOfWorks;

public class UnitOfWork(
    OrderDbContext orderDbContext,
    IOrderRepository orderRepository,
    IOutboxMessagesRepository outboxMessagesRepository)
    : IUnitOfWork
{
    public IOrderRepository Orders { get; } = orderRepository;
    public IOutboxMessagesRepository OutboxMessages { get; } = outboxMessagesRepository;

    public async Task SaveChangesAsync()
        => await orderDbContext.SaveChangesAsync();
}