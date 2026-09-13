using System.Text.Json;
using OrderFlow.Order.Application.Abstractions.Services;
using OrderFlow.Order.Application.Abstractions.UnitOfWorks;
using OrderFlow.Order.Application.Constants;
using OrderFlow.Order.Application.Dtos;
using OrderFlow.Order.Application.Events;
using OrderFlow.Order.Domain.Entities;

namespace OrderFlow.Order.Application.Services;

using Order = Domain.Entities.Order;

public class OrderService(IUnitOfWork unitOfWork) : IOrderService
{
    public async Task<CreateOrderResponseDto> CreateAsync(CreateOrderDto dto)
    {
        var order = Order.Create(dto.CustomerId);

        var orderLines = dto.OrderLines
            .Select(ol =>
                OrderLine.Create(
                    order.Id,
                    ol.Sku,
                    ol.Quantity,
                    ol.UnitPrice))
            .ToList();

        var orderSagaState = OrderSagaState.Create(order.Id);

        order.AddRange(orderLines);

        order.AddState(orderSagaState);

        await unitOfWork.Orders.AddAsync(order);

        var @event = new OrderPlacedEvent
        {
            OrderId = order.Id,
            CustomerId = order.CustomerId,
            OrderLines = order
                .OrderLines
                .Select(ol => new OrderPlacedOrderLineEvent
                {
                    Sku = ol.Sku,
                    Quantity = ol.Quantity,
                    UnitPrice = ol.UnitPrice
                })
                .ToList(),
            TotalAmount = order.TotalAmount
        };

        var outboxMessage = OutboxMessage.Create(
            TopicName.OrderPlaced,
            JsonSerializer.SerializeToDocument(@event),
            order.Id);

        await unitOfWork.OutboxMessages.AddAsync(outboxMessage);

        await unitOfWork.SaveChangesAsync();

        var response = new CreateOrderResponseDto
        {
            OrderId = order.Id,
            CorrelationId = order.Id,
            Status = order.Status.ToString()
        };

        return response;
    }

    public async Task<GetByIdResponseDto?> GetByIdAsync(Guid orderId)
    {
        var order = await unitOfWork.Orders.GetByIdAsync(orderId);

        if (order is null)
        {
            return null;
        }

        var response = new GetByIdResponseDto
        {
            OrderId = order.Id,
            CustomerId = order.CustomerId,
            Status = order.Status.ToString(),
            TotalAmount = order.TotalAmount,
            ReservationCompleted = order.OrderSagaState.ReservationCompleted,
            PaymentCompleted = order.OrderSagaState.PaymentCompleted,
            OrderLines = order
                .OrderLines
                .Select(ol => new GetByIdOrderLineResponseDto
                {
                    Sku = ol.Sku,
                    Quantity = ol.Quantity,
                    UnitPrice = ol.UnitPrice
                })
                .ToList(),
            CreatedAt = order.CreatedAt,
            UpdatedAt = order.UpdatedAt
        };

        return response;
    }

    public async Task<GetByCustomerIdResponseDto?> GetByCustomerIdAsync(string customerId)
    {
        var order = await unitOfWork.Orders.GetByCustomerIdAsync(customerId);

        if (order is null)
        {
            return null;
        }

        var response = new GetByCustomerIdResponseDto()
        {
            OrderId = order.Id,
            Status = order.Status.ToString(),
            TotalAmount = order.TotalAmount,
            CreatedAt = order.CreatedAt
        };

        return response;
    }
}