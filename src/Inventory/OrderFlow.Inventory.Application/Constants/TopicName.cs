namespace OrderFlow.Inventory.Application.Constants;

public static class TopicName
{
    public const string DeadLetter = "dlq";
    public const string OrderPlaced = "order-placed";
    public const string ReservationFailed = "reservation-failed";
    public const string ReservationSucceeded = "reservation-succeeded";
}