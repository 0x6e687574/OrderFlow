using System.Net.Http.Json;
using OrderFlow.Client.Clients.Abstractions;
using OrderFlow.Client.Dtos.Inventory;

namespace OrderFlow.Client.Clients;

public class InventoryClient(HttpClient httpClient) : IInventoryClient
{
    public async Task<IEnumerable<StockItemDto>> GetStockItemsAsync()
        => await httpClient.GetFromJsonAsync<IEnumerable<StockItemDto>>("stock")
           ?? throw new InvalidOperationException();
}