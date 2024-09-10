using MusShop.Domain.Model.Entities.Blog;
using MusShop.Domain.Model.RepositoryAbstractions.Blog;
using MusShop.Persistence.Repositories.Base;

namespace MusShop.Persistence.Repositories.Blog;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(MusShopDataDbContext context) : base(context)
    {
    }
}