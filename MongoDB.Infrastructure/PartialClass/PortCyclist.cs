namespace MongoDB.Infrastructure;

using System;

public partial class PortCyclist
{
    public override bool Equals(object? obj) => obj is PortCyclist ciclista && NamePort == ciclista.NamePort;
    public override int GetHashCode() => HashCode.Combine(NamePort);
}
