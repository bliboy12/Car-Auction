using MediatR;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResult>
{
    private readonly IIdentityService _identityService;
    private readonly ITokenGenerator _tokenGenerator;
    public LoginCommandHandler(IIdentityService identityService, ITokenGenerator tokenGenerator)
    {
        _identityService = identityService;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = await _identityService.ValidateCredentialsAsync(request.Email, request.Password);
        if (!result.Succeeded)
            throw new ArgumentException("Invalid credentials");

        var (token, expiresAt) = _tokenGenerator.GenerateToken(result.Value, request.Email);
        return new LoginResult(token, expiresAt);
    }
}