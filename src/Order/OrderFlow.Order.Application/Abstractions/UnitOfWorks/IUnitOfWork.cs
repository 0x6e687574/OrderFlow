using OrderFlow.Order.Application.Abstractions.Repositories;

namespace OrderFlow.Order.Application.Abstractions.UnitOfWorks;

public interface IUnitOfWork
{
    public IOrderRepository Orders { get; }
    public Task SaveChangesAsync();
}