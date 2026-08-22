public sealed record AuctionDto(Guid id, DateTime startTime, DateTime endTime, Guid carId, Guid sellerId, AuctionStatus status, decimal currentPrice, decimal startingPrice);
