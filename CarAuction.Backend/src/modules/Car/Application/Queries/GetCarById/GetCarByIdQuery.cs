using MediatR;

public class GetCarByIdQuery : IRequest<CarDto>
{
    public Guid Id { get; private set; }

    public GetCarByIdQuery(Guid id)
    {
        Id = id;
    }
}