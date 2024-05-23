namespace MongoDB.Infrastructure;

using System.ComponentModel.DataAnnotations.Schema;

public partial class Maillot : IIdString
{
    [Column("_id")]
    public string Id { get; set; } = string.Empty;
    [Column("tipo")]
    public string Type { get; set; } = string.Empty;
    [Column("color")]
    public string Color { get; set; } = string.Empty;
    [Column("premio")]
    public int Reward { get; set; } = 0;

    public Maillot()
    {
    }
}
