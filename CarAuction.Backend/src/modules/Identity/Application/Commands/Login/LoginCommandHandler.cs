public class LoginCommandHandler
{
    private readonly IIdentityService _identityService;
    private readonly ITokenGenerator _tokenGenerator;
    public LoginCommandHandler(IIdentityService identityService, ITokenGenerator tokenGenerator)
    {
        _identityService = identityService;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<LoginResult> Handle(LoginCommand request)
    {
        var result = await _identityService.ValidateCredentialsAsync(request.Email, request.Password);
        if (!result.Succeeded)
            throw new ArgumentException("Invalid credentials");

        var (token, expiresAt) = _tokenGenerator.GenerateToken(result.Value, request.Email);
        return new LoginResult(token, expiresAt);
    }
}