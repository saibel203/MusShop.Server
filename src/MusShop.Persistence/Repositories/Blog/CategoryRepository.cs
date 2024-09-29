using MusShop.Contracts.Filters;
using MusShop.Contracts.RepositoryAbstractions.Blog;
using MusShop.Domain.Model.Entities.Blog;
using MusShop.Persistence.Repositories.Base;

namespace MusShop.Persistence.Repositories.Blog;

public class CategoryRepository : GenericRepository<Category, CategoryFilter>, ICategoryRepository
{
    public CategoryRepository(MusShopDataDbContext context) : base(context)
    {
    }
}