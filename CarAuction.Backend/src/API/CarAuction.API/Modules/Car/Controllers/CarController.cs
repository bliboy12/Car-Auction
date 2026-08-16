using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/cars")]
public class CarController : ControllerBase
{
    private readonly IMediator _mediator;

    public CarController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateCar([FromBody] CarRequest request)
    {
        Guid sellerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var command = new CreateCarCommand(request.Brand, request.Model, request.Trim, request.Year, request.Kilometers, request.HasDamage, request.Description, request.Color, request.Fuel, sellerId);

        Guid carId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetCarById), new { id = carId }, new { id = carId });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCarById([FromRoute] Guid id)
    {
        CarDto dto = await _mediator.Send(new GetCarByIdQuery(id));
        return Ok(dto);
    }

}