using MapsterMapper;
using MediatR;
using MusShop.Application.Dtos.Blog.Category;
using MusShop.Application.UseCases.Commons;
using MusShop.Contracts.Filters;
using MusShop.Contracts.RepositoryAbstractions.Base;
using MusShop.Domain.Model.Entities.Blog;
using MusShop.Domain.Model.ResultItems;

namespace MusShop.Application.UseCases.Features.Blog.Categories.Commands.CreateCategoryCommand;

public class CreateCategoryHandler : BaseFeatureConfigs,
    IRequestHandler<CreateCategoryCommand, DomainResult<CategoryDto>>
{
    public CreateCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(
        unitOfWork, mapper)
    {
    }

    public async Task<DomainResult<CategoryDto>> Handle(CreateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        Category createdCategory = Mapper.Map<Category>(request.CategoryActionDto);

        await UnitOfWork.GetRepository<Category, CategoryFilter>().Add(createdCategory);
        await UnitOfWork.CommitAsync(cancellationToken);

        CategoryDto newCategoryDto = Mapper.Map<CategoryDto>(createdCategory);

        return DomainResult<CategoryDto>.Success(newCategoryDto);
    }
}