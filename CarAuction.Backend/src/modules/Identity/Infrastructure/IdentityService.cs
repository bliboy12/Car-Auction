using Microsoft.AspNetCore.Identity;

public sealed class IdentityService : IIdentityService
{
    private UserManager<IdentityUser> _userManager;
    public IdentityService(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }
    public async Task<Result<Guid>> CreateUserAsync(string email, string password)
    {
        var user = new IdentityUser
        {
            UserName = email,
            Email = email
        };

        IdentityResult result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);
            return Result<Guid>.Failure(errors);
        }
        return Result<Guid>.Success(Guid.Parse(user.Id));
    }

    public async Task<Result<Guid>> ValidateCredentialsAsync(string email, string password)
    {
        IdentityUser? user = await _userManager.FindByEmailAsync(email);
        IEnumerable<string> err = ["Login Failed, Email or/and Password invalid"];

        if (user is null)
            return Result<Guid>.Failure(err);

        bool validPassword = await _userManager.CheckPasswordAsync(user, password);

        if (!validPassword)
            return Result<Guid>.Failure(err);

        return Result<Guid>.Success(Guid.Parse(user.Id));
    }

    public async Task<Result<string>> GetEmailByIdAsync(Guid id)
    {
        IEnumerable<string> err = ["User not found"];
        IdentityUser? user = await _userManager.FindByIdAsync(id.ToString());

        if (user is null)
            return Result<string>.Failure(err);

        return Result<string>.Success(user.Email!);

    }
}