using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB.Models
{
    public class Puerto : PuertoO
    {
        public override string nompuerto { get; set; } = "";
        public int altura { get; set; } = 0;
        public string categoria { get; set; } = "";
        public double pendiente { get; set; } = 0;

        public Puerto()
        {
        }

        public Puerto(PuertoO puertoO)
        {
            _id = puertoO.nompuerto;
            Puerto? puerto = Repository.GetPorts().Where(x => x._id.Equals(puertoO.nompuerto)).FirstOrDefault();
            if (puerto != null)
            {
                altura = puerto.altura;
                categoria = puerto.categoria;
                pendiente = puerto.pendiente;
            }
            else
            {
                altura = 0;
                categoria = "";
                pendiente = 0.0;
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is Puerto puerto &&
                   base.Equals(obj) &&
                   nompuerto == puerto.nompuerto &&
                   _id == puerto._id &&
                   altura == puerto.altura &&
                   categoria == puerto.categoria &&
                   pendiente == puerto.pendiente;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), nompuerto, _id, altura, categoria, pendiente);
        }
    }
}
