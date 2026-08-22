using MediatR;

public sealed class RegisterClientCommandHandler : IRequestHandler<RegisterClientCommand, LoginResult>
{
    private readonly IIdentityService _identityService;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClientProfileRepository _clientRepo;
    public RegisterClientCommandHandler(IIdentityService identityService, ITokenGenerator tokenGenerator, IUnitOfWork unitOfWork, IClientProfileRepository clientRepo)
    {
        _identityService = identityService;
        _tokenGenerator = tokenGenerator;
        _unitOfWork = unitOfWork;
        _clientRepo = clientRepo;
    }

    public async Task<LoginResult> Handle(RegisterClientCommand request, CancellationToken cancellationToken)
    {
        var identityResult = await _identityService.CreateUserAsync(request.Email, request.Password);

        if (!identityResult.Succeeded)
            throw new ArgumentException(String.Join(";", identityResult.Errors));

        Guid userId = identityResult.Value;

        ClientProfile clientProfile = ClientProfile.CreateClientProfile(userId, request.FirstName, request.LastName, request.BirthDate, request.Address);

        await _clientRepo.AddAsync(clientProfile);
        await _unitOfWork.SaveChangesAsync();

        var (token, expiresAt) = _tokenGenerator.GenerateToken(userId, request.Email);

        return new LoginResult(token, expiresAt);
    }
}