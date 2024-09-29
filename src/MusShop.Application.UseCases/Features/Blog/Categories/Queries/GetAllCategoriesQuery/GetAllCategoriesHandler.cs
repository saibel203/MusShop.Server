using MapsterMapper;
using MediatR;
using MusShop.Application.Dtos.Blog.Category;
using MusShop.Application.UseCases.Commons;
using MusShop.Contracts.Filters;
using MusShop.Contracts.RepositoryAbstractions.Base;
using MusShop.Domain.Model.Entities.Blog;
using MusShop.Domain.Model.ResultItems;

namespace MusShop.Application.UseCases.Features.Blog.Categories.Queries.GetAllCategoriesQuery;

public class GetAllCategoriesHandler
    : BaseFeatureConfigs, IRequestHandler<GetAllCategoriesQuery, DomainResult<IEnumerable<CategoryDto>>>
{
    public GetAllCategoriesHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<DomainResult<IEnumerable<CategoryDto>>> Handle(GetAllCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        IEnumerable<Category> categories =
            await UnitOfWork.GetRepository<Category, CategoryFilter>().GetAll();
        IEnumerable<CategoryDto> categoriesDto = Mapper.Map<IEnumerable<CategoryDto>>(categories);

        return DomainResult<IEnumerable<CategoryDto>>.Success(categoriesDto);
    }
}