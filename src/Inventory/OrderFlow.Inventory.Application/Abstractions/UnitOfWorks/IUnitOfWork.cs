using OrderFlow.Inventory.Application.Abstractions.Repositories;

namespace OrderFlow.Inventory.Application.Abstractions.UnitOfWorks;

public interface IUnitOfWork
{
    public IStockItemRepository StockItems { get; }
    public IOutboxMessageRepository  OutboxMessages { get; }
    
    public Task SaveChangesAsync();
}