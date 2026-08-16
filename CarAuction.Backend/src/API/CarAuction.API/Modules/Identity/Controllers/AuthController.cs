using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly RegisterClientCommandHandler _registerHandler;
    private readonly LoginCommandHandler _loginHandler;

    public AuthController(RegisterClientCommandHandler registerHandler, LoginCommandHandler loginHandler)
    {
        _registerHandler = registerHandler;
        _loginHandler = loginHandler;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        var command = new RegisterClientCommand(request.Email, request.Password, request.FirstName, request.LastName, request.BirthDate, request.Address);
        LoginResult result = await _registerHandler.Handle(command, cancellationToken);

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var command = new LoginCommand(request.Email, request.Password);
        LoginResult result = await _loginHandler.Handle(command, cancellationToken);

        return Ok(result);
    }
}