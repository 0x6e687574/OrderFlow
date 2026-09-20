using OrderFlow.Payment.Application.Abstractions.Services;
using OrderFlow.Payment.Application.Abstractions.UnitOfWorks;
using OrderFlow.Payment.Application.Dtos;

namespace OrderFlow.Payment.Application.Services;

public class PaymentService(IUnitOfWork unitOfWork) : IPaymentService
{
    public async Task<GetPaymentByOrderIdResponseDto?> GetByOrderIdAsync(Guid orderId)
    {
        var payment = await unitOfWork.Payments.GetByOrderIdAsync(orderId);

        if (payment is null)
        {
            return null;
        }

        var response = new GetPaymentByOrderIdResponseDto
        {
            PaymentId = payment.Id,
            OrderId = payment.OrderId,
            Amount = payment.Amount,
            Status = payment.Status.ToString(),
            CreatedAt = payment.CreatedAt
        };

        return response;
    }
}