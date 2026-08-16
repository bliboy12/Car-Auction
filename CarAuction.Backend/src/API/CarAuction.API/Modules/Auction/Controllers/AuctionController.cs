using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auctions")]
public class AuctionController : ControllerBase
{
    private readonly IMediator _mediator;
    public AuctionController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateAuction([FromBody] CreateAuctionRequest request)
    {
        Guid sellerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var command = new CreateAuctionCommand(request.StartTime, request.EndTime, request.CarId, sellerId, request.StartingPrice);
        Guid auctionId = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetAuctionById), new { id = auctionId }, new { id = auctionId });
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAuctionById([FromRoute] Guid id)
    {
        AuctionDto dto = await _mediator.Send(new GetAuctionByIdQuery(id));
        return Ok(dto);
    }

    [Authorize]
    [HttpPost("{id}/bids")]
    public async Task<IActionResult> PlaceBid([FromRoute] Guid id, [FromBody] PlaceBidRequest request)
    {
        Guid bidderId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var command = new PlaceBidCommand(id, bidderId, request.Amount);
        Guid bidId = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetBidsByAuctionId), new { id }, new { id = bidId });
    }
    [HttpGet("{id}/bids")]
    public async Task<IActionResult> GetBidsByAuctionId([FromRoute] Guid id)
    {
        IEnumerable<BidDto> dtos = await _mediator.Send(new GetBidsByAuctionIdQuery(id));

        return Ok(dtos);
    }
}