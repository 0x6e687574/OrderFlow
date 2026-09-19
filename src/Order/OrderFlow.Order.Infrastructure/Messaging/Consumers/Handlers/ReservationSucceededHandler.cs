using Microsoft.EntityFrameworkCore;
using OrderFlow.Order.Application.Events;
using OrderFlow.Order.Domain.Entities;
using OrderFlow.Order.Infrastructure.Persistence;

namespace OrderFlow.Order.Infrastructure.Messaging.Consumers.Handlers;

public class ReservationSucceededHandler(OrderDbContext orderDbContext)
{
    public async Task HandleAsync(ReservationSucceededEvent payload, CancellationToken cancellationToken)
    {
        if (await IsProcessed(payload.EventId, cancellationToken))
        {
            return;
        }

        var order = await orderDbContext.Orders
            .Include(o => o.OrderSagaState)
            .FirstOrDefaultAsync(
                o => o.Id == payload.OrderId,
                cancellationToken);

        if (order is null)
        {
            return;
        }

        order.OrderSagaState.Reserve(payload.EventId, true);
        order.Charge();

        await MarkAsProcessedAsync(payload.EventId, cancellationToken);
        await SaveChangesAsync(cancellationToken);
    }

    private Task<bool> IsProcessed(Guid eventId, CancellationToken cancellationToken)
        => orderDbContext.InboxMessages
            .AnyAsync(
                im => im.EventId == eventId,
                cancellationToken);

    private async Task MarkAsProcessedAsync(Guid eventId, CancellationToken cancellationToken)
    {
        var inboxMessage = InboxMessage.Create(eventId);

        await orderDbContext.InboxMessages.AddAsync(inboxMessage, cancellationToken);
    }

    private Task SaveChangesAsync(CancellationToken cancellationToken)
        => orderDbContext.SaveChangesAsync(cancellationToken);
}