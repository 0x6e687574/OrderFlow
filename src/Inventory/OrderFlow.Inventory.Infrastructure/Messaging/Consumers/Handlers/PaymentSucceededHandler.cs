using Microsoft.EntityFrameworkCore;
using OrderFlow.Inventory.Application.Events;
using OrderFlow.Inventory.Domain.Entities;
using OrderFlow.Inventory.Infrastructure.Persistence;

namespace OrderFlow.Inventory.Infrastructure.Messaging.Consumers.Handlers;

public class PaymentSucceededHandler(InventoryDbContext inventoryDbContext)
{
    public async Task HandleAsync(PaymentSucceededEvent payload, CancellationToken cancellationToken)
    {
        if (await IsProcessed(payload.EventId, cancellationToken))
        {
            return;
        }

        var reservations = await inventoryDbContext.Reservations
            .Where(r => r.OrderId == payload.OrderId)
            .ToHashSetAsync(cancellationToken);

        if (IsEmptyReservations(reservations))
        {
            return;
        }

        var skus = reservations
            .Select(r => r.Sku)
            .ToHashSet();

        var stockItems = await inventoryDbContext.StockItems
            .Where(si => skus.Contains(si.Sku))
            .ToHashSetAsync(cancellationToken);

        var consumedQuantities = reservations.ToDictionary(
            r => r.Sku,
            r => r.Quantity);

        UpdateReservationStatuses(reservations);
        UpdateStockItemQuantities(stockItems, consumedQuantities);

        await MarkAsProcessedAsync(payload.EventId, cancellationToken);
        await SaveChangesAsync(cancellationToken);
    }

    private async Task<bool> IsProcessed(Guid eventId, CancellationToken cancellationToken)
        => await inventoryDbContext.InboxMessages
            .AnyAsync(
                im => im.EventId == eventId,
                cancellationToken);

    private async Task MarkAsProcessedAsync(Guid eventId, CancellationToken cancellationToken)
    {
        var inboxMessage = InboxMessage.Create(eventId);

        await inventoryDbContext.InboxMessages.AddAsync(inboxMessage, cancellationToken);
    }

    private async Task SaveChangesAsync(CancellationToken cancellationToken)
        => await inventoryDbContext.SaveChangesAsync(cancellationToken);

    private static bool IsEmptyReservations(HashSet<Reservation> reservations)
        => reservations is not { Count: > 0 };

    private static void UpdateReservationStatuses(HashSet<Reservation> reservations)
    {
        foreach (var reservation in reservations)
        {
            reservation.Consume();
        }
    }

    private static void UpdateStockItemQuantities(
        HashSet<StockItem> stockItems,
        Dictionary<string, int> consumedQuantities)
    {
        foreach (var stockItem in stockItems)
        {
            stockItem.Consume(consumedQuantities[stockItem.Sku]);
        }
    }
}