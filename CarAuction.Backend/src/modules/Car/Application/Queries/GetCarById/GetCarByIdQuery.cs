using MediatR;

public sealed record GetCarByIdQuery(Guid Id) : IRequest<CarDto>;