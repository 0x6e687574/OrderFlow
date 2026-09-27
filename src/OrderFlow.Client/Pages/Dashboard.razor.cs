using Microsoft.AspNetCore.Components;
using OrderFlow.Client.Clients.Abstractions;
using OrderFlow.Client.Dtos.Inventory;
using OrderFlow.Client.Dtos.Order;

namespace OrderFlow.Client.Pages;

public partial class Dashboard : ComponentBase
{
    [Inject] public IOrderClient OrderClient { get; set; } = null!;
    [Inject] public IInventoryClient InventoryClient { get; set; } = null!;

    private IEnumerable<StockItemDto> _stockItems = [];
    private IEnumerable<GetRecentOrderDto> _recentOrders = [];

    protected override void OnInitialized()
    {
        _ = GetDashboard();
    }

    private async Task GetDashboard()
    {
        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(5));

        while (await timer.WaitForNextTickAsync())
        {
            _stockItems = await InventoryClient.GetStockItemsAsync();
            _recentOrders = await OrderClient.GetRecentOrdersAsync();
            await InvokeAsync(StateHasChanged);
        }
    }
}