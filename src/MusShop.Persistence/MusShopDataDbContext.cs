using Microsoft.EntityFrameworkCore;
using MusShop.Domain.Model.Entities.Blog;

namespace MusShop.Persistence;

public class MusShopDataDbContext : DbContext
{
    public MusShopDataDbContext(DbContextOptions<MusShopDataDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}