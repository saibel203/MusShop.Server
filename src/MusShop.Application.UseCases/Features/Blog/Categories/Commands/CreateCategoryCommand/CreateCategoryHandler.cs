using MapsterMapper;
using MediatR;
using MusShop.Application.Dtos.Blog.Category;
using MusShop.Application.UseCases.Commons;
using MusShop.Domain.Model.Entities.Blog;
using MusShop.Domain.Model.RepositoryAbstractions.Base;
using MusShop.Domain.Model.ResultItems;
using MusShop.Domain.Model.ResultItems.ErrorsDocumentation;

namespace MusShop.Application.UseCases.Features.Blog.Categories.Commands.CreateCategoryCommand;

public class CreateCategoryHandler : BaseFeatureConfigs,
    IRequestHandler<CreateCategoryCommand, DomainResult<CategoryDto>>
{
    public CreateCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<DomainResult<CategoryDto>> Handle(CreateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        if (request.CategoryActionDto.CategoryName.Length is 0 or > 100)
        {
            return DomainResult<CategoryDto>.Failure(BlogErrors.CategoryNameError);
        }

        Category createdCategory = Mapper.Map<Category>(request.CategoryActionDto);

        await UnitOfWork.GetRepository<Category>().Add(createdCategory);
        await UnitOfWork.CommitAsync(cancellationToken);

        CategoryDto newCategoryDto = Mapper.Map<CategoryDto>(createdCategory);

        return DomainResult<CategoryDto>.Success(newCategoryDto);
    }
}