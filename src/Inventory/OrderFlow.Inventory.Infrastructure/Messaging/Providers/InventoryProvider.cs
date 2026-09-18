using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderFlow.Inventory.Application.Abstractions.Messaging;
using OrderFlow.Inventory.Application.Constants;
using OrderFlow.Inventory.Domain.Entities;
using OrderFlow.Inventory.Infrastructure.Persistence;

namespace OrderFlow.Inventory.Infrastructure.Messaging.Providers;

public class InventoryProvider(IServiceProvider serviceProvider, IEventBus eventBus) : BackgroundService
{
    private const int MaxHandle = 10;

    private readonly TimeSpan _period = TimeSpan.FromSeconds(5);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = serviceProvider.CreateScope();

        await using var inventoryDbContext = scope
            .ServiceProvider
            .GetRequiredService<InventoryDbContext>();

        using var timer = new PeriodicTimer(_period);

        while (!stoppingToken.IsCancellationRequested &&
               await timer.WaitForNextTickAsync(stoppingToken))
        {
            var outboxMessages = await inventoryDbContext.OutboxMessages
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
                    outboxMessage.Topic,
                    outboxMessage.CorrelationId.ToString(),
                    outboxMessage.Payload.RootElement.GetRawText());

                outboxMessage.Publish();

                await inventoryDbContext.SaveChangesAsync(stoppingToken);
            }
        }
    }

    private static bool IsEmptyOutboxMessages(IReadOnlyCollection<OutboxMessage> outboxMessages)
        => outboxMessages is not { Count: > 0 };
}