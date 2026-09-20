using OrderFlow.Payment.Application.Dtos;

namespace OrderFlow.Payment.Application.Abstractions.Services;

public interface IPaymentService
{
    public Task<GetPaymentByOrderIdResponseDto?> GetByOrderIdAsync(Guid orderId);
}