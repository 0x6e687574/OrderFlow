namespace OrderFlow.Order.Application.Abstractions.Messaging;

public interface IEventBus
{
    public Task PublishAsync(string topic, string correlationId, string payload);
}