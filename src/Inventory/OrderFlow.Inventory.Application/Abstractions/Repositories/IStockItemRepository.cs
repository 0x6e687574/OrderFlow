using OrderFlow.Inventory.Domain.Entities;

namespace OrderFlow.Inventory.Application.Abstractions.Repositories;

public interface IStockItemRepository
{
    public Task AddAsync(StockItem stockItem);
    public Task<IReadOnlyCollection<StockItem>> GetAllAsync();
}