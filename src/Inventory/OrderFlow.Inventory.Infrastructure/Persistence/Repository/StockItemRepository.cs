using Microsoft.EntityFrameworkCore;
using OrderFlow.Inventory.Application.Abstractions.Repositories;
using OrderFlow.Inventory.Domain.Entities;

namespace OrderFlow.Inventory.Infrastructure.Persistence.Repository;

public class StockItemRepository(InventoryDbContext inventoryDbContext) : IStockItemRepository
{
    public async Task AddAsync(StockItem stockItem)
        => await inventoryDbContext.StockItems.AddAsync(stockItem);

    public async Task<StockItem?> GetAsync(string sku)
        => await inventoryDbContext.StockItems.FindAsync(sku);

    public async Task<IReadOnlyCollection<StockItem>> GetAllAsync()
        => await inventoryDbContext.StockItems.ToListAsync();

    public async Task<bool> ExistsAsync(string sku)
        => await inventoryDbContext.StockItems.AnyAsync(si => si.Sku == sku);
}