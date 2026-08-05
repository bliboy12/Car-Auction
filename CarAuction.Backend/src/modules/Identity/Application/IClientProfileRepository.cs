public interface IClientProfileRepository
{
    Task AddAsync(ClientProfile client);
    Task<ClientProfile?> GetByIdAsync(Guid userId);
}