using Microsoft.EntityFrameworkCore;
using Sensorialab.Application.Interfaces;

namespace Sensorialab.Infrastructure.Data;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Configurations will be added here
    }
}
