using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB.Models
{
    public class Equipo : IIdString
    {
        public string _id { get; set; } = "";
        public string director { get; set; } = "";

        public Equipo()
        {
        }

        public Equipo(string id, string director)
        {
            _id = id;
            this.director = director;
        }

        public Equipo(CommutatorDTO commutador)
        {
            Equipo? equipo = Repository.GetTeams().Where(x => x._id.Equals(commutador.col2)).FirstOrDefault();
            if (equipo != null)
            {
                this._id = equipo._id;
                this.director = equipo.director;
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is Equipo equipo &&
                   _id == equipo._id &&
                   director == equipo.director;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_id, director);
        }
    }
}
