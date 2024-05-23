namespace MongoDB.Infrastructure;

using System;

public partial class Port
{
    public Port(PortCyclist portCyclist)
    {
        Id = portCyclist.NamePort;
        Port? port = Repository.GetPorts().Find(x => x.Id.Equals(portCyclist.NamePort));
        if (port != null)
        {
            Height = port.Height;
            Category = port.Category;
            Slope = port.Slope;
        }
        else
        {
            Height = 0;
            Category = string.Empty;
            Slope = 0;
        }
    }

    public override bool Equals(object? obj) => obj is Port puerto && Id == puerto.Id && Height == puerto.Height && Category == puerto.Category && Slope == puerto.Slope;
    public override int GetHashCode() => HashCode.Combine(Id, Height, Category, Slope);
}
