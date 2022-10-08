using Microsoft.EntityFrameworkCore;
using MoreTech.Data;

namespace MoreTech.Migrate.Database.Services;

internal class MigrateDatabaseService : IMigrateDatabaseService
{
    private readonly DataContext context;

    public MigrateDatabaseService(DataContext context)
    {
        this.context = context;
    }

    public async Task Migrate() => await context.Database.MigrateAsync();

    public async Task Recreate()
    {
        await context.Database.EnsureDeletedAsync();
        await Migrate();
        await context.Database.EnsureCreatedAsync();
    }
}
