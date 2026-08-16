using MediatR;

public class LoginCommand : IRequest<LoginResult>
{
    public string Email { get; } = string.Empty;
    public string Password { get; } = string.Empty;

    public LoginCommand(string email, string password)
    {
        Email = email;
        Password = password;
    }
}