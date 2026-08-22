using MediatR;

public sealed record GetClientProfileByIdQuery(Guid Id) : IRequest<ClientProfileDto>;