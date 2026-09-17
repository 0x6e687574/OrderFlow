using OrderFlow.Inventory.Infrastructure.Exceptions.Abstractions;

namespace OrderFlow.Inventory.Infrastructure.Exceptions;

public sealed class DeserializeFailedException : InfrastructureException
{
    private new const string Message = "Failed to deserialize!";

    public DeserializeFailedException()
        : base(Message)
    {
    }

    public DeserializeFailedException(string message)
        : base(message)
    {
    }

    public DeserializeFailedException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}