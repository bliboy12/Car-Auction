public class CreateClientProfileCommandHandler
{
    private IClientProfileRepository _clientRepo;
    private IUnitOfWork _unitOfWork;

    public CreateClientProfileCommandHandler(IClientProfileRepository clientProfile, IUnitOfWork unitOfWork)
    {
        _clientRepo = clientProfile;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateClientProfileCommand request)
    {
        ClientProfile clientProfile = ClientProfile.CreateClientProfile(request.Id, request.FirstName, request.LastName, request.BirthDate, request.Address);

        await _clientRepo.AddAsync(clientProfile);
        await _unitOfWork.SaveChangesAsync();

        return clientProfile.Id;
    }
}