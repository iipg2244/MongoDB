namespace MongoDB.Models;

using MongoDB.Bson.Serialization.Attributes;

public class MaillotO : MaillotT
{
    public override string? codigo
    {
        get => _id;
        set => _id = value;
    }

    [BsonIgnoreIfNull]
    public string? _id { get; set; } = null;

    public MaillotO()
    {
    }

    public MaillotO(Maillot maillot, Etapa etapa)
    {
        this.netapa = etapa._id;
        this.codigo = maillot._id;
    }

    public override bool Equals(object? obj) => obj is MaillotO o &&
               codigo == o.codigo &&
               _id == o._id &&
               netapa == o.netapa;

    public override int GetHashCode() => HashCode.Combine(codigo, _id, netapa);
}
