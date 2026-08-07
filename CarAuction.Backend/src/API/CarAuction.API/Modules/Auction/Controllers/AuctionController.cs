using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auctions")]
public class AuctionController : ControllerBase
{
    private CreateAuctionCommandHandler _createCommandHandler;
    private PlaceBidCommandHandler _placeBidCommandHandler;
    private GetAuctionByIdQueryHandler _getAuctionByIdQueryHandler;
    private GetBidsByAuctionIdQueryHandler _getBidsByAuctionIdQueryHandler;
    public AuctionController(CreateAuctionCommandHandler createAuctionCommandHandler, PlaceBidCommandHandler placeBidCommandHandler, GetAuctionByIdQueryHandler getAuctionByIdQueryHandler, GetBidsByAuctionIdQueryHandler getBidsByAuctionIdQueryHandler)
    {
        _createCommandHandler = createAuctionCommandHandler;
        _placeBidCommandHandler = placeBidCommandHandler;
        _getAuctionByIdQueryHandler = getAuctionByIdQueryHandler;
        _getBidsByAuctionIdQueryHandler = getBidsByAuctionIdQueryHandler;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateAuctionAsync([FromBody] CreateAuctionRequest request)
    {
        Guid sellerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var command = new CreateAuctionCommand(request.StartTime, request.EndTime, request.CarId, sellerId, request.StartingPrice);
        Guid auctionId = await _createCommandHandler.Handle(command);

        return CreatedAtAction(nameof(GetAuctionByIdAsync), new { id = auctionId }, new { id = auctionId });
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAuctionByIdAsync([FromRoute] Guid id)
    {
        AuctionDto dto = await _getAuctionByIdQueryHandler.Handle(new GetAuctionByIdQuery(id));
        return Ok(dto);
    }

    [HttpPost("{id}/bids")]
    public async Task<IActionResult> PlaceBidAsync([FromRoute] Guid id, [FromBody] PlaceBidRequest request)
    {
        Guid bidderId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var command = new PlaceBidCommand(id, bidderId, request.Amount);
        Guid bidId = await _placeBidCommandHandler.Handle(command);

        return CreatedAtAction(nameof(GetBidsByAuctionIdAsync), new { id }, new { id = bidId });
    }
    [HttpGet("{id}/bids")]
    public async Task<IActionResult> GetBidsByAuctionIdAsync([FromRoute] Guid id)
    {
        IEnumerable<BidDto> dtos = await _getBidsByAuctionIdQueryHandler.Handle(new GetBidsByAuctionIdQuery(id));

        return Ok(dtos);
    }
}