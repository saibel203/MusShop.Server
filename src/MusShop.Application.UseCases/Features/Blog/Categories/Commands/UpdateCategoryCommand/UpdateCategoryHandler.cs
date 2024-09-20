using MapsterMapper;
using MediatR;
using MusShop.Application.Dtos.Blog.Category;
using MusShop.Application.UseCases.Commons;
using MusShop.Domain.Model.Entities.Blog;
using MusShop.Domain.Model.RepositoryAbstractions.Base;
using MusShop.Domain.Model.ResultItems;
using MusShop.Domain.Model.ResultItems.ErrorsDocumentation;

namespace MusShop.Application.UseCases.Features.Blog.Categories.Commands.UpdateCategoryCommand;

public class UpdateCategoryHandler : BaseFeatureConfigs,
    IRequestHandler<UpdateCategoryCommand, DomainResult<CategoryActionDto>>
{
    public UpdateCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    
    public async Task<DomainResult<CategoryActionDto>> Handle(UpdateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        Category? category = await UnitOfWork.GetRepository<Category>().GetById(request.CategoryId);

        if (category is null)
        {
            return DomainResult<CategoryActionDto>.Failure(BlogErrors.CategoryNotFound);
        }

        Mapper.Map(request.CategoryActionDto, category);

        UnitOfWork.GetRepository<Category>().Update(category);
        await UnitOfWork.CommitAsync(cancellationToken);

        return DomainResult<CategoryActionDto>.Success(request.CategoryActionDto);
    }
}