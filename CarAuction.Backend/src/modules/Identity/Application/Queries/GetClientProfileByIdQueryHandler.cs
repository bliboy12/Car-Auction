public class GetClientProfileByIdQueryHandler
{
    private IClientProfileRepository _clientRepo;

    public GetClientProfileByIdQueryHandler(IClientProfileRepository clientRepo)
    {
        _clientRepo = clientRepo;
    }

    public async Task<ClientProfileDto> Handle(GetClientProfileByIdQuery request)
    {
        ClientProfile clientProfile = await _clientRepo.GetByIdAsync(request.Id);

        if (String.IsNullOrWhiteSpace(clientProfile.FirstName))
            throw new ArgumentException("Firstname can not be empty");
        if (String.IsNullOrWhiteSpace(clientProfile.LastName))
            throw new ArgumentException("Lastname can not be empty");

        return new ClientProfileDto(clientProfile.Id, clientProfile.FirstName, clientProfile.LastName, clientProfile.BirthDate, clientProfile.Address);
    }
}