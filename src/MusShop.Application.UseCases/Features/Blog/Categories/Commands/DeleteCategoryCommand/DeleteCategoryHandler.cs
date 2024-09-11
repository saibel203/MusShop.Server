using MapsterMapper;
using MediatR;
using MusShop.Application.Dtos.Blog.Category;
using MusShop.Application.UseCases.Commons;
using MusShop.Domain.Model.Entities.Blog;
using MusShop.Domain.Model.RepositoryAbstractions.Base;
using MusShop.Domain.Model.ResultItems;
using MusShop.Domain.Model.ResultItems.ErrorsDocumentation;

namespace MusShop.Application.UseCases.Features.Blog.Categories.Commands.DeleteCategoryCommand;

public class DeleteCategoryHandler : BaseFeatureConfigs,
    IRequestHandler<DeleteCategoryCommand, DomainResult<CategoryDto>>
{
    public DeleteCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    
    public async Task<DomainResult<CategoryDto>> Handle(DeleteCategoryCommand request,
        CancellationToken cancellationToken)
    {
        Category? category = await UnitOfWork.GetRepository<Category>().GetById(request.Id);

        if (category is null)
        {
            return DomainResult<CategoryDto>.Failure(BlogErrors.CategoryNotFound);
        }

        UnitOfWork.GetRepository<Category>().Delete(category);
        await UnitOfWork.CommitAsync(cancellationToken);

        return DomainResult<CategoryDto>.Success(null!);
    }
}