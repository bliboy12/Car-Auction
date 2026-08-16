using MediatR;

public class GetClientProfileByIdQuery : IRequest<ClientProfileDto>
{
    public Guid Id { get; }
    public GetClientProfileByIdQuery(Guid id) => Id = id;
}