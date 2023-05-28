using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB.Models
{
    public class MaillotT
    {
        [BsonIgnoreIfNull]
        public virtual string? codigo { get; set; } = null;

        [BsonIgnoreIfDefault]
        public int netapa { get; set; } = 0;

        public MaillotT()
        {
        }

        public MaillotT(Maillot maillot, Etapa etapa)
        {
            this.netapa = etapa._id;
            this.codigo = maillot._id;
        }

        public override bool Equals(object? obj)
        {
            return obj is MaillotT o &&
                   codigo == o.codigo &&
                   netapa == o.netapa;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(codigo, netapa);
        }
    }
}
