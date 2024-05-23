namespace MongoDB.Infrastructure;

using System.ComponentModel.DataAnnotations.Schema;

public partial class Port : IIdString
{
    [Column("_id")]
    public string Id { get; set; } = string.Empty;
    [Column("altura")]
    public int Height { get; set; } = 0;
    [Column("categoria")]
    public string Category { get; set; } = string.Empty;
    [Column("pendiente")]
    public double Slope { get; set; } = 0;

    public Port()
    {
    }
}
