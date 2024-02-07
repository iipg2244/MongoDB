namespace MongoDB.Models;

using MongoDB.Bson.Serialization.Attributes;

public class EtapaO : EtapaT, IId
{
    public override int netapa
    {
        get => _id;
        set => _id = value;
    }
    [BsonIgnoreIfDefault]
    public int _id { get; set; } = 0;

    public EtapaO()
    {
    }

    public EtapaO(Etapa etapa)
    {
        netapa = etapa._id;
        salida = etapa.salida;
        llegada = etapa.llegada;
    }

    public override bool Equals(object? obj) => obj is EtapaO o &&
               netapa == o.netapa &&
               salida == o.salida &&
               llegada == o.llegada;

    public override int GetHashCode() => HashCode.Combine(netapa, salida, llegada);
}
