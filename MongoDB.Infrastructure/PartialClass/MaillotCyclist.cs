namespace MongoDB.Infrastructure;

using System;

public partial class MaillotCyclist
{
    public MaillotCyclist(Maillot maillot, Phase phase)
    {
        this.NumberPhase = phase.Id;
        this.Code = maillot.Id;
    }

    public override bool Equals(object? obj) => obj is MaillotCyclist ciclista && Code == ciclista.Code && NumberPhase == ciclista.NumberPhase;
    public override int GetHashCode() => HashCode.Combine(Code, NumberPhase);
}
