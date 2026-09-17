using OrderFlow.Inventory.Infrastructure.Exceptions.Abstractions;

namespace OrderFlow.Inventory.Infrastructure.Exceptions;

public sealed class MessageKeyNotFoundException : InfrastructureException
{
    private new const string Message = "Message key not found!";

    public MessageKeyNotFoundException()
        : base(Message)
    {
    }

    public MessageKeyNotFoundException(string message)
        : base(message)
    {
    }

    public MessageKeyNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}