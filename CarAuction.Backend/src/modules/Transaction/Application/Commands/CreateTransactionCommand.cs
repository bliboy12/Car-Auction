public class CreateTransactionCommand
{
    public Guid WinningBidId { get; private set; }
    public PaymentStatus Status { get; private set; }
    public string? PaymentReference { get; private set; }

}