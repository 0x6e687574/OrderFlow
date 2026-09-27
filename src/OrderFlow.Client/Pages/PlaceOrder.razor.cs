using Microsoft.AspNetCore.Components;
using OrderFlow.Client.Clients.Abstractions;
using OrderFlow.Client.Dtos.Inventory;
using OrderFlow.Client.Dtos.Order;

namespace OrderFlow.Client.Pages;

public partial class PlaceOrder : ComponentBase
{
    [Inject] public IOrderClient OrderClient { get; set; } = null!;
    [Inject] public IInventoryClient InventoryClient { get; set; } = null!;

    private readonly CreateOrderRequestDto _createOrderRequestDto = new();

    private CreateOrderResponseDto? _createOrderResponseDto;
    private IEnumerable<StockItemDto> _stockItems = [];
    private GetOrderByIdDto? _getOrderByIdDto;

    private bool _showMessage;

    protected override async Task OnInitializedAsync()
    {
        _stockItems = await InventoryClient.GetStockItemsAsync();
    }

    private void AddLine()
        => _createOrderRequestDto.OrderLines.Add(new CreateOrderLineRequestDto());

    private void RemoveLine(CreateOrderLineRequestDto orderLine)
        => _createOrderRequestDto.OrderLines.Remove(orderLine);

    private void ShowMessage()
        => _showMessage = true;

    private async Task HandleOrder()
    {
        try
        {
            _createOrderResponseDto = await OrderClient.CreateOrderAsync(_createOrderRequestDto);
            _ = FetchOrderInformation();
        }
        catch
        {
            _createOrderResponseDto = null;
        }

        ShowMessage();
    }

    private async Task FetchOrderInformation()
    {
        using var timer = new PeriodicTimer(TimeSpan.FromMilliseconds(500));

        while (await timer.WaitForNextTickAsync())
        {
            try
            {
                _getOrderByIdDto = await OrderClient.GetOrderByIdAsync(_createOrderResponseDto!.OrderId);

                await InvokeAsync(StateHasChanged);

                if (_getOrderByIdDto.Status is "Confirmed" or "Cancelled")
                {
                    break;
                }
            }
            catch
            {
                _getOrderByIdDto = null;
                break;
            }
        }
    }
}