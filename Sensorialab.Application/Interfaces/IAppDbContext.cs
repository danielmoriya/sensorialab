using Microsoft.EntityFrameworkCore;

namespace Sensorialab.Application.Interfaces;

public interface IAppDbContext
{
    // DbSet properties will be added here as entities are defined
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
