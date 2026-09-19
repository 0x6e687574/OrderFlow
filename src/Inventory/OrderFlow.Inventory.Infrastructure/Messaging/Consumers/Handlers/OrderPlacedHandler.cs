using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using OrderFlow.Inventory.Application.Constants;
using OrderFlow.Inventory.Application.Events;
using OrderFlow.Inventory.Domain.Entities;
using OrderFlow.Inventory.Infrastructure.Persistence;

namespace OrderFlow.Inventory.Infrastructure.Messaging.Consumers.Handlers;

public class OrderPlacedHandler(InventoryDbContext inventoryDbContext)
{
    public async Task HandleAsync(OrderPlacedEvent payload, CancellationToken cancellationToken)
    {
        if (IsEmptyOrderLines(payload.OrderLines))
        {
            await HandleReservationFailedAsync(payload.OrderId, cancellationToken);
            return;
        }

        var skus = payload.OrderLines
            .Select(ol => ol.Sku)
            .ToHashSet();

        var stockItems = await inventoryDbContext
            .StockItems
            .Where((si => skus.Contains(si.Sku)))
            .ToHashSetAsync(cancellationToken);

        if (HasInvalidSkus(stockItems, skus))
        {
            await HandleReservationFailedAsync(payload.OrderId, cancellationToken);
            return;
        }

        var availableQuantities = stockItems.ToDictionary(
            si => si.Sku,
            si => si.QuantityOnHand - si.QuantityReserved);

        if (!HasSufficientQuantities(payload.OrderLines, availableQuantities))
        {
            await HandleReservationFailedAsync(payload.OrderId, cancellationToken);
            return;
        }

        await MakeReservations(payload.OrderId, payload.OrderLines, cancellationToken);

        var reservedQuantities = payload.OrderLines.ToDictionary(
            ol => ol.Sku,
            ol => ol.Quantity);

        IncreaseQuantityReserved(stockItems, reservedQuantities);

        await HandleReservationSucceededAsync(
            payload.OrderId,
            payload.OrderLines,
            cancellationToken);
    }

    private static bool IsEmptyOrderLines(IReadOnlyCollection<OrderPlacedOrderLineEvent> orderLines)
        => orderLines is not { Count: > 0 };

    private static bool HasInvalidSkus(HashSet<StockItem> stockItems, HashSet<string> skus)
        => stockItems.Count != skus.Count;

    private static bool HasSufficientQuantities(
        IReadOnlyCollection<OrderPlacedOrderLineEvent> orderLines,
        Dictionary<string, int> availableQuantities)
        => orderLines.All(ol => availableQuantities[ol.Sku] >= ol.Quantity);

    private async Task MakeReservations(
        Guid orderId,
        IReadOnlyCollection<OrderPlacedOrderLineEvent> orderLines,
        CancellationToken cancellationToken)
    {
        var reservations = orderLines
            .Select(ol =>
                Reservation.Create(
                    orderId,
                    ol.Sku,
                    ol.Quantity))
            .ToList();

        await inventoryDbContext.Reservations.AddRangeAsync(reservations, cancellationToken);
    }

    private async Task HandleReservationFailedAsync(Guid orderId, CancellationToken cancellationToken)
    {
        var @event = new ReservationFailedEvent
        {
            OrderId = orderId,
            Reason = "Invalid SKU!"
        };

        var outboxMessage = OutboxMessage.Create(
            @event.EventId,
            TopicName.ReservationFailed,
            JsonSerializer.SerializeToDocument(@event),
            orderId,
            @event.CreatedAt);

        await inventoryDbContext.OutboxMessages.AddAsync(outboxMessage, cancellationToken);

        await inventoryDbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task HandleReservationSucceededAsync(
        Guid orderId,
        IReadOnlyCollection<OrderPlacedOrderLineEvent> orderLines,
        CancellationToken cancellationToken)
    {
        var @event = new ReservationSucceededEvent
        {
            OrderId = orderId,
            OrderLines = orderLines
                .Select(ol => new ReservationSucceededOrderLineEvent
                {
                    Sku = ol.Sku,
                    Quantity = ol.Quantity,
                    UnitPrice = ol.UnitPrice
                })
                .ToList(),
        };

        var outboxMessage = OutboxMessage.Create(
            @event.EventId,
            TopicName.ReservationSucceeded,
            JsonSerializer.SerializeToDocument(@event),
            orderId,
            @event.CreatedAt);

        await inventoryDbContext.OutboxMessages.AddAsync(outboxMessage, cancellationToken);

        await inventoryDbContext.SaveChangesAsync(cancellationToken);
    }

    private void IncreaseQuantityReserved(
        HashSet<StockItem> stockItems,
        Dictionary<string, int> reservedQuantities)
    {
        foreach (var stockItem in stockItems)
        {
            stockItem.Reserve(reservedQuantities[stockItem.Sku]);
        }
    }
}