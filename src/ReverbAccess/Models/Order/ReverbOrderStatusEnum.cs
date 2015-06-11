namespace ReverbAccess.Models.Order
{
    /// <summary>
    /// :unpaid, :payment_pending, :paid, :shipped, :picked_up, :received, :presumed_received, :cancelled, :refunded
    /// </summary>
    public enum ReverbOrderStatusEnum
    {
        Default = 0,
        Unpaid = 1,
        PaymentPending = 2,
        Paid = 3,
        Shipped = 4,
        PickedUp = 5,
        Received = 6,
        PresumedReceived = 7,
        Cancelled = 8,
        Refunded = 9
    }
}
