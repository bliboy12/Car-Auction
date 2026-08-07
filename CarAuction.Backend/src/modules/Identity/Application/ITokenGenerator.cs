public interface ITokenGenerator
{
    // TODO: still needs to be expanded to accept roles for role-based authorization
    (string, DateTime) GenerateToken(Guid userId, string email);
}