using OrderFlow.Client.Dtos.Inventory;

namespace OrderFlow.Client.Clients.Abstractions;

public interface IInventoryClient
{
    public Task<IEnumerable<StockItemDto>> GetStockItemsAsync();
}