namespace MongoDB.Models;

using MongoDB.Bson.Serialization.Attributes;

public class EtapaT
{
    [BsonIgnoreIfDefault]
    public virtual int netapa { get; set; } = 0;
    public string salida { get; set; } = string.Empty;
    public string llegada { get; set; } = string.Empty;

    public EtapaT()
    {
    }

    public EtapaT(Etapa etapa)
    {
        netapa = etapa._id;
        salida = etapa.salida;
        llegada = etapa.llegada;
    }

    public override bool Equals(object? obj) => obj is EtapaT o &&
               netapa == o.netapa &&
               salida == o.salida &&
               llegada == o.llegada;

    public override int GetHashCode() => HashCode.Combine(netapa, salida, llegada);
}
