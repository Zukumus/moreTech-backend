using Microsoft.EntityFrameworkCore;
using MoreTech.Data.Models;

namespace MoreTech.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    /// <summary>
    /// Новости из источника
    /// </summary>
    public DbSet<NewsModel> NewsFromSource { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new NewsModelConfiguration());
    }
}