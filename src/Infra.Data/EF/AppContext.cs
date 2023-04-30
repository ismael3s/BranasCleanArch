using Infra.Data.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.EF;
public class AppDbContext : DbContext
{
    public DbSet<Cupom> Cupoms { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
