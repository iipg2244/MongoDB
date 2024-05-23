namespace MongoDB.Infrastructure;

using System.ComponentModel.DataAnnotations.Schema;

public partial class MaillotCyclist
{
    [Column("codigo")]
    public virtual string Code
    {
        get => Id ?? string.Empty;
        set => Id = value;
    }

    [NotMapped]
    public string? Id { get; set; } = string.Empty;
    [Column("netapa")]
    public int NumberPhase { get; set; } = 0;

    public MaillotCyclist()
    {
    }
}
