public abstract class Entity
{
    public Guid Id { get; protected set; }

    protected Entity() { }
    protected Entity(Guid id) => Id = id;

    public override bool Equals(object? obj)
    {
        // If obj is a Entity, give me a variable Entity named 'other' 
        if (obj is not Entity other) return false;
        // Are these two variables pointing to the exact same object in memory (comparing itself with itself)
        // This is the filter out before doing the more expensive comparison
        if (ReferenceEquals(this, other)) return true;
        // Entity is a shared base for multiple different concrete types.
        // Here we check if the two variables are from the exact same class like 'car' (and not 'car' == 'Auction')
        if (GetType() != other.GetType()) return false;

        // Now we compare based on the actual ID if its the same
        return Id == other.Id;
    }
    // Making sure that the hashcode produced is based on the ID and not the address.
    // This has to be done always after changing up the Equals.
    public override int GetHashCode() => Id.GetHashCode();

}