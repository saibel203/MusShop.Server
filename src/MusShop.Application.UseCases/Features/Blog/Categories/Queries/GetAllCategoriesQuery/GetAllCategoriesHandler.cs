using Mapster;
using MapsterMapper;
using MediatR;
using MusShop.Application.Dtos.Blog.Category;
using MusShop.Application.UseCases.Commons;
using MusShop.Contracts.Filters;
using MusShop.Contracts.RepositoryAbstractions.Base;
using MusShop.Contracts.Responses;
using MusShop.Domain.Model.Entities.Blog;
using MusShop.Domain.Model.ResultItems;

namespace MusShop.Application.UseCases.Features.Blog.Categories.Queries.GetAllCategoriesQuery;

public class GetAllCategoriesHandler
    : BaseFeatureConfigs, IRequestHandler<GetAllCategoriesQuery, DomainResult<PaginatedList<CategoryDto>>>
{
    public GetAllCategoriesHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<DomainResult<PaginatedList<CategoryDto>>> Handle(GetAllCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        PaginatedList<Category> categories =
            await UnitOfWork.GetRepository<Category, CategoryFilter>().GetAll();
        PaginatedList<CategoryDto> categoriesDto = categories.Adapt<PaginatedList<CategoryDto>>();

        return DomainResult<PaginatedList<CategoryDto>>.Success(categoriesDto);
    }
}