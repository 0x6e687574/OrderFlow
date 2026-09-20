using OrderFlow.Order.Application.Dtos;

namespace OrderFlow.Order.Application.Abstractions.Services;

public interface IOrderService
{
    public Task<CreateOrderResponseDto> CreateAsync(CreateOrderDto dto);
    public Task<GetByIdResponseDto?> GetByIdAsync(Guid orderId);
    public Task<IEnumerable<GetByCustomerIdResponseDto>> GetAllByCustomerIdAsync(string customerId);
}