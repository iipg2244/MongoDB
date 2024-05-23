namespace MongoDB.Infrastructure;

using System.ComponentModel.DataAnnotations.Schema;

public partial class PhaseCyclist
{
    [Column("netapa")]
    public virtual int NumberPhase
    {
        get => Id ?? 0;
        set => Id = value;
    }

    [NotMapped]
    public int? Id { get; set; } = 0;
    [Column("salida")]
    public string Start { get; set; } = string.Empty;
    [Column("llegada")]
    public string Finish { get; set; } = string.Empty;

    public PhaseCyclist()
    {
    }
}
