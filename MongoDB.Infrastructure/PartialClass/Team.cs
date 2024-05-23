namespace MongoDB.Infrastructure;

using System;
using MongoDB.Infrastructure.Dtos;

public partial class Team
{
    public Team(string id, string manager)
    {
        Id = id;
        this.Manager = manager;
    }

    public Team(CommutatorDto commutador)
    {
        Team? team = Repository.GetTeam(commutador.Col2);
        if (team != null)
        {
            this.Id = team.Id;
            this.Manager = team.Manager;
        }
    }

    public override bool Equals(object? obj) => obj is Team equipo && Id == equipo.Id && Manager == equipo.Manager;
    public override int GetHashCode() => HashCode.Combine(Id, Manager);
}
