using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB.Models
{
    public class CommutatorDTO
    {
        public int? col1 { get; set; } = null;
        public string col2 { get; set; } = "";
        public int? col3 { get; set; } = null;
        public string col4 { get; set; } = "";
        public int countPuertos { get; set; } = 0;
        public int countMaillots { get; set; } = 0;
        public int countEtapas { get; set; } = 0;
        public int countTodo { get; set; } = 0;

        public CommutatorDTO()
        {
        }

        public CommutatorDTO(Ciclista ciclista)
        {
            col1 = ciclista._id;
            col2 = ciclista.nombre;
            col3 = ciclista.edad;
            col4 = ciclista.nomeq;
            countPuertos = 0;
            countMaillots = 0;
            countEtapas = 0;
            if (ciclista.puertos_g?.Count > 0)
            {
                countPuertos = ciclista.puertos_g.Count;
            }
            if (ciclista.maillots_g?.Count > 0)
            {
                countMaillots = ciclista.maillots_g.Count;
            }
            if (ciclista.etapas_g?.Count > 0)
            {
                countEtapas = ciclista.etapas_g.Count;
            }
            countTodo = countPuertos + countMaillots + countEtapas;
        }

        public CommutatorDTO(Equipo equipo)
        {
            col1 = null;
            col2 = equipo._id;
            col3 = null;
            col4 = equipo.director;
            countPuertos = 0;
            countMaillots = 0;
            countEtapas = 0;
            List<Ciclista> lciclistas = Repository.GetCyclists().Where(x => x.nomeq.Equals(equipo._id)).ToList();
            if (lciclistas?.Count > 0)
            {
                foreach (Ciclista ciclista in lciclistas)
                {
                    if (ciclista.puertos_g?.Count > 0)
                    {
                        countPuertos = countPuertos + ciclista.puertos_g.Count();

                    }
                    if (ciclista.maillots_g?.Count > 0)
                    {
                        countMaillots = countMaillots + ciclista.maillots_g.Count();
                    }
                    if (ciclista.etapas_g?.Count > 0)
                    {
                        countEtapas = countEtapas + ciclista.etapas_g.Count();
                    }
                }
            }
            countTodo = countPuertos + countMaillots + countEtapas;
        }



    }
}
