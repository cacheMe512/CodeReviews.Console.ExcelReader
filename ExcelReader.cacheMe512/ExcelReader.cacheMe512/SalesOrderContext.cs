using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace ExcelReader.cacheMe512;

internal class SalesOrderContext : DbContext
{
    public DbSet<SalesOrder> SalesOrders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var connectionString = configuration.GetConnectionString("SalesOrderDatabase");

        optionsBuilder.UseSqlServer(connectionString)
                      .ConfigureWarnings(warnings =>
                          warnings.Ignore(RelationalEventId.NonTransactionalMigrationOperationWarning));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public static void ResetDatabase()
    {
        using (var context = new SalesOrderContext())
        {
            Console.WriteLine("Deleting existing database...");
            context.Database.EnsureDeleted();

            Console.WriteLine("Creating new database...");
            context.Database.EnsureCreated();
        }
    }
}
