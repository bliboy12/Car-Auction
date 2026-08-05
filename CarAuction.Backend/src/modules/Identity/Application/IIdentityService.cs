public interface IIdentityService
{
    Task<Result<Guid>> CreateUserAsync(string email, string password);
    Task<Result<Guid>> ValidateCredentialsAsync(string email, string password);
}