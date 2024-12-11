namespace MongoDB.Infrastructure;

using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;

public static class CyclingEntitiesFactory
{
    private static CyclingEntities? instanse = null;

    public static CyclingEntities Create(string connectionString, string database)
    {
        if (instanse == null)
        {
            MongoClient client = new MongoClient(connectionString);
            instanse = CyclingEntities.Create(client.GetDatabase(database));
        }
        return instanse;
    }
}

public partial class CyclingEntities : DbContext
{
    public static CyclingEntities Create(IMongoDatabase database) =>
        new(new DbContextOptionsBuilder<CyclingEntities>()
            .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
            .Options);

    public CyclingEntities(DbContextOptions<CyclingEntities> options)
     : base(options)
    {
    }

    public virtual DbSet<Cyclist> Cyclists { get; init; } = null!;
    public virtual DbSet<Phase> Phases { get; init; } = null!;
    public virtual DbSet<Maillot> Maillots { get; init; } = null!;
    public virtual DbSet<Team> Teams { get; init; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Cyclist>().ToCollection("ciclistas");
        modelBuilder.Entity<Phase>().ToCollection("etapas");
        modelBuilder.Entity<Maillot>().ToCollection("maillots");
        modelBuilder.Entity<Team>().ToCollection("equipos");
    }
}

