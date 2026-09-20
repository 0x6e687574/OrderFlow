namespace OrderFlow.Payment.Api.Contracts.Responses;

public record GetPaymentByOrderIdResponse(
    Guid PaymentId,
    Guid OrderId,
    decimal Amount,
    string Status,
    DateTime CreatedAt);