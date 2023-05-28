using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB.Models
{
    public class EtapaT
    {
        [BsonIgnoreIfDefault]
        public virtual int netapa { get; set; } = 0;
        public string salida { get; set; } = "";
        public string llegada { get; set; } = "";

        public EtapaT()
        {
        }

        public EtapaT(Etapa etapa)
        {
            netapa = etapa._id;
            salida = etapa.salida;
            llegada = etapa.llegada;
        }

        public override bool Equals(object? obj)
        {
            return obj is EtapaT o &&
                   netapa == o.netapa &&
                   salida == o.salida &&
                   llegada == o.llegada;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(netapa, salida, llegada);
        }
    }
}
