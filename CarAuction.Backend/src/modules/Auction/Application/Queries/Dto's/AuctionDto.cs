public sealed record AuctionDto(Guid Id, DateTime StartTime, DateTime EndTime, Guid CarId, Guid SellerId, AuctionStatus Status, decimal CurrentPrice, decimal StartingPrice, Guid? WinningBid);
