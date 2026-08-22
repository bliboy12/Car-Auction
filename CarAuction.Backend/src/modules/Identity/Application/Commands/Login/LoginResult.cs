public sealed record LoginResult
{
    public string Token { get; } = string.Empty;
    public DateTime ExpireTime { get; }

    public LoginResult(string token, DateTime expireTime)
    {
        Token = token;
        ExpireTime = expireTime;
    }
}