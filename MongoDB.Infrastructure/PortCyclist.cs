namespace MongoDB.Infrastructure;

using System.ComponentModel.DataAnnotations.Schema;

public partial class PortCyclist
{
    [Column("nompuerto")]
    public virtual string NamePort
    {
        get => Id ?? string.Empty;
        set => Id = value;
    }

    [NotMapped]
    public string? Id { get; set; } = string.Empty;

    public PortCyclist()
    {
    }
}
