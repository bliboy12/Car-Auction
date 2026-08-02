public class Transaction : Entity
{
    public Guid WinningBidId { get; private set; }
    public PaymentStatus Status { get; private set; }
    public string? PaymentReference { get; private set; }

    private Transaction() { } // EF Core

    private Transaction(Guid id, Guid winningBidId, PaymentStatus status, string? paymentReference) : base(id)
    {
        WinningBidId = winningBidId;
        Status = status;
        PaymentReference = paymentReference;
    }

    public static Transaction CreateNewTransaction(Guid winningBidId)
    {
        return new Transaction(Guid.NewGuid(), winningBidId, PaymentStatus.Pending, null);
    }

    public void MarkAsPaid(string paymentReference)
    {
        if (Status != PaymentStatus.Pending)
            throw new ArgumentException("Only a pending transaction can be marked as paid");
        if (string.IsNullOrWhiteSpace(paymentReference))
            throw new ArgumentException("Payment reference is required");

        PaymentReference = paymentReference;
        Status = PaymentStatus.Paid;
    }

    public void MarkAsFailed()
    {
        if (Status != PaymentStatus.Pending)
            throw new ArgumentException("Only a pending transaction can be marked as failed");

        Status = PaymentStatus.Failed;
    }
}