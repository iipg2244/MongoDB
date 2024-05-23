namespace MongoDB.Infrastructure;

using System;

public partial class Phase
{
    public Phase(int id, int distance, string start, string finish)
    {
        Id = id;
        this.Distance = distance;
        this.Start = start;
        this.Finish = finish;
    }

    public Phase(PhaseCyclist phaseCyclist)
    {
        Id = phaseCyclist.NumberPhase;
        Phase? phase = Repository.GetPhase(phaseCyclist.NumberPhase);
        if (phase != null)
        {
            this.Start = phase.Start;
            this.Finish = phase.Finish;
            this.Distance = phase.Distance;
            this.Ports = phase.Ports;
        }
        else
        {
            this.Start = string.Empty;
            this.Finish = string.Empty;
            this.Distance = 0;
            this.Ports = new List<Port>();
        }
    }

    public override bool Equals(object? obj) => obj is Phase etapa && Id == etapa.Id && Distance == etapa.Distance && Start == etapa.Start && Finish == etapa.Finish && EqualityComparer<List<Port>>.Default.Equals(Ports, etapa.Ports);
    public override int GetHashCode() => HashCode.Combine(Id, Distance, Start, Finish, Ports);
}
