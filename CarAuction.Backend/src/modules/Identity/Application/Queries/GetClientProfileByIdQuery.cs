public class GetClientProfileByIdQuery
{
    public Guid Id { get; }
    public GetClientProfileByIdQuery(Guid id) => Id = id;
}