using MediatR;

public sealed class GetClientProfileByIdQueryHandler : IRequestHandler<GetClientProfileByIdQuery, ClientProfileDto>
{
    private IClientProfileRepository _clientRepo;
    private IIdentityService _identityService;

    public GetClientProfileByIdQueryHandler(IClientProfileRepository clientRepo, IIdentityService identityService)
    {
        _clientRepo = clientRepo;
        _identityService = identityService;
    }

    public async Task<ClientProfileDto> Handle(GetClientProfileByIdQuery request, CancellationToken cancellationToken)
    {
        ClientProfile? clientProfile = await _clientRepo.GetByIdAsync(request.Id);

        if (clientProfile is null)
            throw new NotFoundException($"Client with ID: {request.Id} not found");

        var email = await _identityService.GetEmailByIdAsync(request.Id);

        if (!email.Succeeded)
            throw new InvalidOperationException($"Client profile {request.Id} exists but has no matching Identity user — data integrity issue.");


        return new ClientProfileDto(clientProfile.Id, clientProfile.FirstName, clientProfile.LastName, clientProfile.BirthDate, clientProfile.Address, email.Value!);
    }
}