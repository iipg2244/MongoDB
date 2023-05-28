using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB.Models
{
    public class MaillotO : MaillotT
    {
        public override string? codigo
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

        public override bool Equals(object? obj)
        {
            return obj is MaillotO o &&
                   codigo == o.codigo &&
                   _id == o._id &&
                   netapa == o.netapa;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(codigo, _id, netapa);
        }
    }
}
