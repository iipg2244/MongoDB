namespace MongoDB.Models;

using Dto;

public class Ciclista : IId
{
    public int _id { get; set; } = 0;
    public string nombre { get; set; } = string.Empty;
    public int edad { get; set; } = 0;
    public string nomeq { get; set; } = string.Empty;

    public List<MaillotT> maillots_g { get; set; } = new List<MaillotT>();
    public List<EtapaT> etapas_g { get; set; } = new List<EtapaT>();
    public List<PuertoO> puertos_g { get; set; } = new List<PuertoO>();

    public Ciclista()
    {
    }

    public Ciclista(int id, string nombre, int edad, string nomeq)
    {
        _id = id;
        this.nombre = nombre;
        this.edad = edad;
        this.nomeq = nomeq;
    }

    public Ciclista(CommutatorDto commutador)
    {
        Ciclista? ciclista = Repository.GetCyclists().Find(x => x._id == commutador.col1);
        if (ciclista != null)
        {
            _id = ciclista._id;
            nombre = ciclista.nombre;
            edad = ciclista.edad;
            nomeq = ciclista.nomeq;
            maillots_g = ciclista.maillots_g;
            etapas_g = ciclista.etapas_g;
            puertos_g = ciclista.puertos_g;
        }
    }

    public override bool Equals(object? obj) => obj is Ciclista ciclista &&
               _id == ciclista._id &&
               nombre == ciclista.nombre &&
               edad == ciclista.edad &&
               nomeq == ciclista.nomeq &&
               EqualityComparer<List<MaillotT>>.Default.Equals(maillots_g, ciclista.maillots_g) &&
               EqualityComparer<List<EtapaT>>.Default.Equals(etapas_g, ciclista.etapas_g) &&
               EqualityComparer<List<PuertoO>>.Default.Equals(puertos_g, ciclista.puertos_g);

    public override int GetHashCode() => HashCode.Combine(_id, nombre, edad, nomeq, maillots_g, etapas_g, puertos_g);
}
