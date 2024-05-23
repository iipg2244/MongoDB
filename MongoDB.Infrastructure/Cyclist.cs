namespace MongoDB.Infrastructure;

using System.ComponentModel.DataAnnotations.Schema;

public partial class Cyclist : IId
{
    [Column("_id")]
    public int Id { get; set; } = 0;
    [Column("nombre")]
    public string Name { get; set; } = string.Empty;
    [Column("edad")]
    public int Age { get; set; } = 0;
    [Column("nomeq")]
    public string TeamName { get; set; } = string.Empty;

    [Column("maillots_g")]
    public List<MaillotCyclist> Maillots { get; set; } = new List<MaillotCyclist>();
    [Column("etapas_g")]
    public List<PhaseCyclist> Phases { get; set; } = new List<PhaseCyclist>();
    [Column("puertos_g")]
    public List<PortCyclist> Ports { get; set; } = new List<PortCyclist>();

    public Cyclist()
    {
    }
}
