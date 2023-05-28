using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB.Models
{
    public class Etapa : EtapaO
    {
        public override int netapa { get; set; } = 0;
        public int km { get; set; } = 0;
        public List<Puerto> puertos { get; set; } = new List<Puerto>();

        public Etapa()
        {
        }

        public Etapa(int id, int km, string salida, string llegada)
        {
            _id = id;
            this.km = km;
            this.salida = salida;
            this.llegada = llegada;
        }

        public Etapa(EtapaO etapaO)
        {
            _id = etapaO.netapa;
            Etapa? etapa = Repository.GetPhases().Where(x => x._id.Equals(etapaO.netapa)).FirstOrDefault();
            if (etapa != null)
            {
                this.salida = etapa.salida;
                this.llegada = etapa.llegada;
                this.km = etapa.km;
                this.puertos = etapa.puertos;
            }
            else{
                this.salida = "";
                this.llegada = "";
                this.km = 0;
                this.puertos = new List<Puerto>();
            }
        }

        public Etapa(EtapaT etapaO)
        {
            _id = etapaO.netapa;
            Etapa? etapa = Repository.GetPhases().Where(x => x._id.Equals(etapaO.netapa)).FirstOrDefault();
            if (etapa != null)
            {
                this.salida = etapa.salida;
                this.llegada = etapa.llegada;
                this.km = etapa.km;
                this.puertos = etapa.puertos;
            }
            else
            {
                this.salida = "";
                this.llegada = "";
                this.km = 0;
                this.puertos = new List<Puerto>();
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is Etapa etapa &&
                   base.Equals(obj) &&
                   _id == etapa._id &&
                   salida == etapa.salida &&
                   llegada == etapa.llegada &&
                   km == etapa.km &&
                   EqualityComparer<List<Puerto>>.Default.Equals(puertos, etapa.puertos);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), _id, salida, llegada, km, puertos);
        }
    }
}
