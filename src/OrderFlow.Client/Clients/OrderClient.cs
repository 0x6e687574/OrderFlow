using System.Net.Http.Json;
using OrderFlow.Client.Clients.Abstractions;
using OrderFlow.Client.Dtos.Order;

namespace OrderFlow.Client.Clients;

public class OrderClient(HttpClient httpClient) : IOrderClient
{
    public async Task<CreateOrderResponseDto> CreateOrderAsync(CreateOrderRequestDto dto)
    {
        var responseMessage = await httpClient.PostAsJsonAsync("orders", dto);
        responseMessage.EnsureSuccessStatusCode();
        var response = await responseMessage.Content.ReadFromJsonAsync<CreateOrderResponseDto>();
        return response!;
    }

    public async Task<GetOrderByIdDto> GetOrderByIdAsync(Guid orderId)
        => await httpClient.GetFromJsonAsync<GetOrderByIdDto>($"orders/{orderId}")
           ?? throw new InvalidOperationException();

    public async Task<IEnumerable<GetRecentOrderDto>> GetRecentOrdersAsync()
        => await httpClient.GetFromJsonAsync<IEnumerable<GetRecentOrderDto>>("orders/recent")
           ?? throw new InvalidOperationException();
}