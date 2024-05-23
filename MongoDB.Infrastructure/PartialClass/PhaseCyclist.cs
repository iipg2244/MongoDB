namespace MongoDB.Infrastructure;

using System;

public partial class PhaseCyclist
{
    public PhaseCyclist(Phase phase)
    {
        NumberPhase = phase.Id;
        Start = phase.Start;
        Finish = phase.Finish;
    }

    public override bool Equals(object? obj) => obj is PhaseCyclist ciclista && NumberPhase == ciclista.NumberPhase && Start == ciclista.Start && Finish == ciclista.Finish;
    public override int GetHashCode() => HashCode.Combine(NumberPhase, Start, Finish);
}
