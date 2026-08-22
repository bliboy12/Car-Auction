using MediatR;

public sealed record RegisterClientCommand(string Email, string Password, string FirstName, string LastName, DateOnly BirthDate, Address Address) : IRequest<LoginResult>;