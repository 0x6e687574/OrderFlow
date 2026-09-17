using OrderFlow.Inventory.Application.Abstractions.Repositories;
using OrderFlow.Inventory.Application.Abstractions.UnitOfWorks;

namespace OrderFlow.Inventory.Infrastructure.Persistence.UnitOfWorks;

public class UnitOfWork(
    InventoryDbContext inventoryDbContext,
    IStockItemRepository stockItemRepository,
    IOutboxMessageRepository outboxMessageRepository)
    : IUnitOfWork
{
    public IStockItemRepository StockItems => stockItemRepository;
    public IOutboxMessageRepository OutboxMessages => outboxMessageRepository;

    public async Task SaveChangesAsync()
        => await inventoryDbContext.SaveChangesAsync();
}