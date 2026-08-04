public class GetCarByIdQuery
{
    public Guid Id { get; private set; }

    public GetCarByIdQuery(Guid id)
    {
        Id = id;
    }
}