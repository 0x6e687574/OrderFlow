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
    public async Task CreateAsync(OrderDto dto)
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

        order.AddRange(orderLines);

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
    }
}