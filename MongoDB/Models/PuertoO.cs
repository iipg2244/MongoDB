namespace MongoDB.Models;

using MongoDB.Bson.Serialization.Attributes;

public class PuertoO : IIdString
{
    [BsonIgnoreIfDefault]
    public virtual string nompuerto
    {
        get => _id;
        set => _id = value;
    }
    [BsonIgnoreIfDefault]
    public string _id { get; set; } = string.Empty;

    public PuertoO()
    {
    }

    public override bool Equals(object? obj) => obj is PuertoO o &&
               nompuerto == o.nompuerto &&
               _id == o._id;

    public override int GetHashCode() => HashCode.Combine(nompuerto, _id);
}
