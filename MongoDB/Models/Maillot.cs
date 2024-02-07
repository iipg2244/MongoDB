namespace MongoDB.Models;

public class Maillot : MaillotO
{
    public override string? codigo { get; set; } = null;
    public string tipo { get; set; } = string.Empty;
    public string color { get; set; } = string.Empty;
    public int premio { get; set; } = 0;

    public Maillot()
    {
    }

    public Maillot(string id, string tipo, string color, int premio)
    {
        _id = id;
        this.tipo = tipo;
        this.color = color;
        this.premio = premio;
    }

    public Maillot(MaillotO maillotO)
    {
        _id = (maillotO.codigo != null ? maillotO.codigo: string.Empty);
        Maillot? maillot = Repository.GetMaillots().Find(x => (x._id != null ? x._id : string.Empty).Equals(maillotO.codigo));
        if (maillot != null)
        {
            tipo = maillot.tipo;
            color = maillot.color;
            premio = maillot.premio;
        }
        else
        {
            tipo = string.Empty;
            color = string.Empty;
            premio = 0;
        }
    }

    public Maillot(MaillotT maillotO)
    {
        _id = (maillotO.codigo != null ? maillotO.codigo : string.Empty);
        Maillot? maillot = Repository.GetMaillots().Find(x => (x._id != null ? x._id : string.Empty).Equals(maillotO.codigo));
        if (maillot != null)
        {
            tipo = maillot.tipo;
            color = maillot.color;
            premio = maillot.premio;
        }
        else
        {
            tipo = string.Empty;
            color = string.Empty;
            premio = 0;
        }
    }

    public override bool Equals(object? obj) => obj is Maillot maillot &&
               base.Equals(obj) &&
               codigo == maillot.codigo &&
               _id == maillot._id &&
               tipo == maillot.tipo &&
               color == maillot.color &&
               premio == maillot.premio;

    public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), codigo, _id, tipo, color, premio);
}
