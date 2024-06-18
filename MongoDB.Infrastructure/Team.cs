namespace MongoDB.Infrastructure;

using System.ComponentModel.DataAnnotations.Schema;

public partial class Team : IIdString
{
    [Column("_id")]
    public string Id { get; set; } = string.Empty;
    [Column("director")]
    public string Manager { get; set; } = string.Empty;
    [Column("directorBackup")]
    public string ManagerBackup { get; set; } = string.Empty;

    public Team()
    {
    }
}
