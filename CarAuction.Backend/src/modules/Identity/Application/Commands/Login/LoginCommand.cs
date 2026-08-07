public class LoginCommand
{
    public string Email { get; } = string.Empty;
    public string Password { get; } = string.Empty;

    public LoginCommand(string email, string password)
    {
        Email = email;
        Password = password;
    }
}