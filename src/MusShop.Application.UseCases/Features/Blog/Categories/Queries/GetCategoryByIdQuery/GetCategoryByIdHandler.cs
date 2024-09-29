using MapsterMapper;
using MediatR;
using MusShop.Application.Dtos.Blog.Category;
using MusShop.Application.UseCases.Commons;
using MusShop.Contracts.Filters;
using MusShop.Contracts.RepositoryAbstractions.Base;
using MusShop.Domain.Model.Entities.Blog;
using MusShop.Domain.Model.ResultItems;
using MusShop.Domain.Model.ResultItems.ErrorsDocumentation;

namespace MusShop.Application.UseCases.Features.Blog.Categories.Queries.GetCategoryByIdQuery;

public class GetCategoryByIdHandler
    : BaseFeatureConfigs, IRequestHandler<GetCategoryByIdQuery, DomainResult<CategoryDto>>
{
    public GetCategoryByIdHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<DomainResult<CategoryDto>> Handle(GetCategoryByIdQuery request,
        CancellationToken cancellationToken)
    {
        Category? category = await UnitOfWork.GetRepository<Category, CategoryFilter>().GetById(request.Id);

        if (category is null)
        {
            return DomainResult<CategoryDto>.Failure(BlogErrors.CategoryNotFound);
        }

        CategoryDto categoryDto = Mapper.Map<CategoryDto>(category);

        return DomainResult<CategoryDto>.Success(categoryDto);
    }
}