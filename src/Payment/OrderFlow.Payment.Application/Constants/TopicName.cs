namespace OrderFlow.Payment.Application.Constants;

public static class TopicName
{
    public const string DeadLetter = "dlq";
    public const string ReservationSucceeded = "reservation-succeeded";
    public const string PaymentSucceeded = "payment-succeeded";
    public const string PaymentFailed = "payment-failed";
}