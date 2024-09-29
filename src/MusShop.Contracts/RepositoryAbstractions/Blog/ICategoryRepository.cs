using MusShop.Contracts.Filters;
using MusShop.Contracts.RepositoryAbstractions.Base;
using MusShop.Domain.Model.Entities.Blog;

namespace MusShop.Contracts.RepositoryAbstractions.Blog;

public interface ICategoryRepository : IGenericRepository<Category, CategoryFilter>
{
}