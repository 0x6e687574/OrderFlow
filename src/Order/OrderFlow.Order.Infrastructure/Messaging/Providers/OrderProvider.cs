using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderFlow.Order.Application.Abstractions.Messaging;
using OrderFlow.Order.Application.Constants;
using OrderFlow.Order.Domain.Entities;
using OrderFlow.Order.Infrastructure.Persistence;

namespace OrderFlow.Order.Infrastructure.Messaging.Providers;

public class OrderProvider(IServiceProvider serviceProvider, IEventBus eventBus) : BackgroundService
{
    private const int MaxHandle = 10;

    private readonly TimeSpan _period = TimeSpan.FromSeconds(5);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = serviceProvider.CreateScope();

        await using var orderDbContext = scope
            .ServiceProvider
            .GetRequiredService<OrderDbContext>();

        using var timer = new PeriodicTimer(_period);

        while (!stoppingToken.IsCancellationRequested &&
               await timer.WaitForNextTickAsync(stoppingToken))
        {
            var outboxMessages = await orderDbContext
                .OutboxMessages
                .Where(om => om.PublishedAt == null)
                .OrderBy(om => om.CreatedAt)
                .Take(MaxHandle)
                .ToListAsync(stoppingToken);

            if (IsEmptyOutboxMessages(outboxMessages))
            {
                continue;
            }

            foreach (var outboxMessage in outboxMessages)
            {
                await eventBus.PublishAsync(
                    TopicName.DeadLetter,
                    outboxMessage.CorrelationId.ToString(),
                    outboxMessage.Payload.RootElement.GetRawText());

                outboxMessage.Publish();
                    
                await orderDbContext.SaveChangesAsync(stoppingToken);
            }
        }
    }

    private static bool IsEmptyOutboxMessages(IReadOnlyCollection<OutboxMessage> outboxMessages)
        => outboxMessages is not { Count: > 0 };
}