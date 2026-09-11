using OrderFlow.Order.Application.Abstractions.Repositories;
using OrderFlow.Order.Application.Abstractions.UnitOfWorks;

namespace OrderFlow.Order.Infrastructure.Persistence.UnitOfWorks;

public class UnitOfWork(OrderDbContext orderDbContext, IOrderRepository orders)
    : IUnitOfWork
{
    public IOrderRepository Orders { get; } = orders;

    public async Task SaveChangesAsync()
        => await orderDbContext.SaveChangesAsync();
}