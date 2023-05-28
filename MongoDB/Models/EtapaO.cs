using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB.Models
{
    public class EtapaO : EtapaT, IId
    {
        public override int netapa
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
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

        public override bool Equals(object? obj)
        {
            return obj is EtapaO o &&
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
