namespace MongoDB.Models;

public class Puerto : PuertoO
{
    public override string nompuerto { get; set; } = string.Empty;
    public int altura { get; set; } = 0;
    public string categoria { get; set; } = string.Empty;
    public double pendiente { get; set; } = 0;

    public Puerto()
    {
    }

    public Puerto(PuertoO puertoO)
    {
        _id = puertoO.nompuerto;
        Puerto? puerto = Repository.GetPorts().Find(x => x._id.Equals(puertoO.nompuerto));
        if (puerto != null)
        {
            altura = puerto.altura;
            categoria = puerto.categoria;
            pendiente = puerto.pendiente;
        }
        else
        {
            altura = 0;
            categoria = string.Empty;
            pendiente = 0.0;
        }
    }

    public override bool Equals(object? obj) => obj is Puerto puerto &&
               base.Equals(obj) &&
               nompuerto == puerto.nompuerto &&
               _id == puerto._id &&
               altura == puerto.altura &&
               categoria == puerto.categoria &&
               pendiente == puerto.pendiente;

    public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), nompuerto, _id, altura, categoria, pendiente);
}
