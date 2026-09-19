namespace OrderFlow.Inventory.Application.Exceptions;

public sealed class DuplicatedStockItemsException : ApplicationException
{
    private new const string Message = "Duplicated stock items!";

    public DuplicatedStockItemsException()
        : base(Message)
    {
    }

    public DuplicatedStockItemsException(string message)
        : base(message)
    {
    }

    public DuplicatedStockItemsException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}