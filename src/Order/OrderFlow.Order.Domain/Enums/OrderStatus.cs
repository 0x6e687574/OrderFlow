namespace OrderFlow.Order.Domain.Enums;

public enum OrderStatus
{
    None = 0,
    Pending,
    Reserving,
    Charging,
    Confirmed,
    Cancelled
}