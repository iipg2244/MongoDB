namespace MongoDB.Infrastructure;

using System;

public partial class Maillot
{
    public Maillot(string id, string type, string color, int reward)
    {
        Id = id;
        this.Type = type;
        this.Color = color;
        this.Reward = reward;
    }

    public Maillot(MaillotCyclist maillotCyclist)
    {
        Id = maillotCyclist.Code;
        Maillot? maillot = Repository.GetMaillot(maillotCyclist.Code);
        if (maillot != null)
        {
            Type = maillot.Type;
            Color = maillot.Color;
            Reward = maillot.Reward;
        }
        else
        {
            Type = string.Empty;
            Color = string.Empty;
            Reward = 0;
        }
    }

    public override bool Equals(object? obj) => obj is Maillot maillot && Id == maillot.Id && Type == maillot.Type && Color == maillot.Color && Reward == maillot.Reward;
    public override int GetHashCode() => HashCode.Combine(Id, Type, Color, Reward);
}
