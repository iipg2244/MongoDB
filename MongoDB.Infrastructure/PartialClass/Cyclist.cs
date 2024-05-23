namespace MongoDB.Infrastructure;

using System;
using System.Collections.Generic;
using MongoDB.Infrastructure.Dtos;

public partial class Cyclist
{
    public Cyclist(int id, string name, int age, string teamName)
    {
        Id = id;
        this.Name = name;
        this.Age = age;
        this.TeamName = teamName;
    }


    public Cyclist(CommutatorDto commutador)
    {
        Cyclist? cyclist = Repository.GetCyclist(commutador.Col1);
        if (cyclist != null)
        {
            Id = cyclist.Id;
            Name = cyclist.Name;
            Age = cyclist.Age;
            TeamName = cyclist.TeamName;
            Maillots = cyclist.Maillots;
            Phases = cyclist.Phases;
            Ports = cyclist.Ports;
        }
    }

    public override bool Equals(object? obj) => obj is Cyclist ciclista && Id == ciclista.Id && Name == ciclista.Name && Age == ciclista.Age && TeamName == ciclista.TeamName && EqualityComparer<List<MaillotCyclist>>.Default.Equals(Maillots, ciclista.Maillots) && EqualityComparer<List<PhaseCyclist>>.Default.Equals(Phases, ciclista.Phases) && EqualityComparer<List<PortCyclist>>.Default.Equals(Ports, ciclista.Ports);
    public override int GetHashCode() => HashCode.Combine(Id, Name, Age, TeamName, Maillots, Phases, Ports);
}
