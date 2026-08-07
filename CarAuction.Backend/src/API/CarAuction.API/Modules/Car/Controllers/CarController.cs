using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/cars")]
public class CarController : ControllerBase
{
    private readonly GetCarByIdQueryHandler _getCarCommand;
    private readonly CreateCarCommandHandler _createCarCommand;

    public CarController(GetCarByIdQueryHandler getCarCommand, CreateCarCommandHandler createCarCommand)
    {
        _getCarCommand = getCarCommand;
        _createCarCommand = createCarCommand;
    }
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateCar([FromBody] CarRequest request)
    {
        Guid sellerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var command = new CreateCarCommand(request.Brand, request.Model, request.Trim, request.Year, request.Kilometers, request.HasDamage, request.Description, request.Color, request.Fuel, sellerId);

        Guid carId = await _createCarCommand.Handle(command);
        return CreatedAtAction(nameof(GetCarById), new { id = carId }, new { id = carId });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCarById([FromRoute] Guid id)
    {
        CarDto dto = await _getCarCommand.Handle(new GetCarByIdQuery(id));
        return Ok(dto);
    }

}