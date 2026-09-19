using OrderFlow.Inventory.Domain.Entities;

namespace OrderFlow.Inventory.Application.Abstractions.Repositories;

public interface IStockItemRepository
{
    public Task AddAsync(StockItem stockItem);
    public Task<StockItem?> GetAsync(string sku);
    public Task<IReadOnlyCollection<StockItem>> GetAllAsync();
    public Task<bool> ExistsAsync(string sku);
}