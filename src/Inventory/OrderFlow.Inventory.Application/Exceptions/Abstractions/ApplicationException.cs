namespace OrderFlow.Inventory.Application.Exceptions.Abstractions;

public abstract class ApplicationException : Exception
{
    protected ApplicationException()
    {
    }

    protected ApplicationException(string message)
        : base(message)
    {
    }

    protected ApplicationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}