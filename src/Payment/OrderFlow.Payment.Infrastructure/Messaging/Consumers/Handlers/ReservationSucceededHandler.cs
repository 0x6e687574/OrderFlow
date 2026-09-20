using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using OrderFlow.Payment.Application.Constants;
using OrderFlow.Payment.Application.Events;
using OrderFlow.Payment.Domain.Entities;
using OrderFlow.Payment.Domain.Enums;
using OrderFlow.Payment.Infrastructure.Persistence;

namespace OrderFlow.Payment.Infrastructure.Messaging.Consumers.Handlers;

using Payment = Domain.Entities.Payment;

public class ReservationSucceededHandler(PaymentDbContext paymentDbContext)
{
    public async Task HandleAsync(ReservationSucceededEvent payload, CancellationToken cancellationToken)
    {
        if (await IsProcessed(payload.EventId, cancellationToken))
        {
            return;
        }

        if (IsEmptyOrderLines(payload.OrderLines))
        {
            await MakePayment(
                payload.OrderId,
                0.00m,
                false,
                cancellationToken);
            await HandlePaymentFailedAsync(payload.OrderId, cancellationToken);
            await MarkAsProcessedAsync(payload.EventId, cancellationToken);
            await SaveChangesAsync(cancellationToken);
            return;
        }

        var amount = payload.OrderLines.Sum(ol => ol.Quantity * ol.UnitPrice);

        if (IsEnding99(amount))
        {
            await MakePayment(
                payload.OrderId,
                amount,
                false,
                cancellationToken);
            await HandlePaymentFailedAsync(payload.OrderId, cancellationToken);
            await MarkAsProcessedAsync(payload.EventId, cancellationToken);
            await SaveChangesAsync(cancellationToken);
            return;
        }

        var payment = await MakePayment(payload.OrderId,
            amount,
            true,
            cancellationToken);
        await HandlePaymentSucceededAsync(
            payload.OrderId,
            payment.Id,
            amount,
            cancellationToken);
        await MarkAsProcessedAsync(payload.EventId, cancellationToken);
        await SaveChangesAsync(cancellationToken);
    }

    private async Task<bool> IsProcessed(Guid eventId, CancellationToken cancellationToken)
        => await paymentDbContext.InboxMessages
            .AnyAsync(
                im => im.EventId == eventId,
                cancellationToken);

    private async Task MarkAsProcessedAsync(Guid eventId, CancellationToken cancellationToken)
    {
        var inboxMessage = InboxMessage.Create(eventId);

        await paymentDbContext.InboxMessages.AddAsync(inboxMessage, cancellationToken);
    }

    private async Task SaveChangesAsync(CancellationToken cancellationToken)
        => await paymentDbContext.SaveChangesAsync(cancellationToken);

    private static bool IsEmptyOrderLines(IReadOnlyCollection<ReservationSucceededOrderLineEvent> orderLines)
        => orderLines is not { Count: > 0 };

    private static bool IsEnding99(decimal amount)
        => amount * 100 % 100 == 99;

    private async Task HandlePaymentFailedAsync(Guid orderId, CancellationToken cancellationToken)
    {
        var @event = new PaymentFailedEvent
        {
            OrderId = orderId,
            Reason = "Ending in 99!"
        };

        var outboxMessage = OutboxMessage.Create(
            @event.EventId,
            TopicName.PaymentFailed,
            JsonSerializer.SerializeToDocument(@event),
            orderId,
            @event.CreatedAt);

        await paymentDbContext.OutboxMessages.AddAsync(outboxMessage, cancellationToken);
    }

    private async Task HandlePaymentSucceededAsync(
        Guid orderId,
        Guid paymentId,
        decimal amount,
        CancellationToken cancellationToken)
    {
        var @event = new PaymentSucceededEvent
        {
            OrderId = orderId,
            PaymentId = paymentId,
            Amount = amount
        };

        var outboxMessage = OutboxMessage.Create(
            @event.EventId,
            TopicName.PaymentSucceeded,
            JsonSerializer.SerializeToDocument(@event),
            orderId,
            @event.CreatedAt);

        await paymentDbContext.OutboxMessages.AddAsync(outboxMessage, cancellationToken);
    }

    private async Task<Payment> MakePayment(
        Guid orderId,
        decimal amount,
        bool isSuccess,
        CancellationToken cancellationToken)
    {
        var payment = Payment.Create(
            orderId,
            amount,
            isSuccess ? PaymentStatus.Succeeded : PaymentStatus.Failed);

        await paymentDbContext.Payments.AddAsync(payment, cancellationToken);

        return payment;
    }
}