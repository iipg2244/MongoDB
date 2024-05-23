namespace MongoDB.Infrastructure;

using System.ComponentModel.DataAnnotations.Schema;

public partial class Phase : IId
{
    [Column("_id")]
    public int Id { get; set; } = 0;
    [Column("km")]
    public int Distance { get; set; } = 0;
    [Column("salida")]
    public string Start { get; set; } = string.Empty;
    [Column("llegada")]
    public string Finish { get; set; } = string.Empty;

    [Column("puertos")]
    public List<Port> Ports { get; set; } = new List<Port>();

    public Phase()
    {
    }
}
