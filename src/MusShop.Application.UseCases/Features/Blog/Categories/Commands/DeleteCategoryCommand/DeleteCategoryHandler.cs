using MediatR;
using MusShop.Application.Dtos.Blog.Category;
using MusShop.Domain.Model.Entities.Blog;
using MusShop.Domain.Model.RepositoryAbstractions.Base;
using MusShop.Domain.Model.ResultItems;
using MusShop.Domain.Model.ResultItems.ErrorsDocumentation;

namespace MusShop.Application.UseCases.Features.Blog.Categories.Commands.DeleteCategoryCommand;

public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, DomainResult<CategoryDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategoryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<DomainResult<CategoryDto>> Handle(DeleteCategoryCommand request,
        CancellationToken cancellationToken)
    {
        Category? category = await _unitOfWork.GetRepository<Category>().GetById(request.Id);

        if (category is null)
        {
            return DomainResult<CategoryDto>.Failure(BlogErrors.CategoryNotFound);
        }

        _unitOfWork.GetRepository<Category>().Delete(category);
        await _unitOfWork.CommitAsync(cancellationToken);

        return DomainResult<CategoryDto>.Success(null!);
    }
}