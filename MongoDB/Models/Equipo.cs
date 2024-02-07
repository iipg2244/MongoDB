namespace MongoDB.Models;

using Dto;

public class Equipo : IIdString
{
    public string _id { get; set; } = string.Empty;
    public string director { get; set; } = string.Empty;

    public Equipo()
    {
    }

    public Equipo(string id, string director)
    {
        _id = id;
        this.director = director;
    }

    public Equipo(CommutatorDto commutador)
    {
        Equipo? equipo = Repository.GetTeams().Find(x => x._id.Equals(commutador.col2));
        if (equipo != null)
        {
            this._id = equipo._id;
            this.director = equipo.director;
        }
    }

    public override bool Equals(object? obj) => obj is Equipo equipo &&
               _id == equipo._id &&
               director == equipo.director;

    public override int GetHashCode() => HashCode.Combine(_id, director);
}
