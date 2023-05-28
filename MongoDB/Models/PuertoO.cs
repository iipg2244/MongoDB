using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB.Models
{
    public class PuertoO : IIdString
    {
        [BsonIgnoreIfDefault]
        public virtual string nompuerto
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
        public string _id { get; set; } = "";

        public PuertoO()
        {
        }

        public override bool Equals(object? obj)
        {
            return obj is PuertoO o &&
                   nompuerto == o.nompuerto &&
                   _id == o._id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(nompuerto, _id);
        }
    }
}
