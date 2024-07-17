using Microsoft.EntityFrameworkCore;

namespace MusShop.Infrastructure.Database;

public class MusShopInfrastructureDbContext : DbContext
{
    public MusShopInfrastructureDbContext(DbContextOptions<MusShopInfrastructureDbContext> options) : base(options)
    {
    }
}