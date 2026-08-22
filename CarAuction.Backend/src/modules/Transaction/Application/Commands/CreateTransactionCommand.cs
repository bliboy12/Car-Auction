using MediatR;

public sealed record CreateTransactionCommand(Guid WinningBidId, PaymentStatus Status, string? PaymentReference) : IRequest<Guid>;