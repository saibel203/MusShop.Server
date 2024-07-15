using Microsoft.EntityFrameworkCore;

namespace MusShop.Infrastructure.Database;

public class MusShopInfrastructureDbContext(DbContextOptions<MusShopInfrastructureDbContext> options)
    : DbContext(options);