public class CreateAuctionRequest
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public Guid CarId { get; set; }
    public decimal StartingPrice { get; set; }
}