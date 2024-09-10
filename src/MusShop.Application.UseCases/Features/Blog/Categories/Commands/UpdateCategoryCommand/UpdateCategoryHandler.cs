using MapsterMapper;
using MediatR;
using MusShop.Application.Dtos.Blog.Category;
using MusShop.Domain.Model.Entities.Blog;
using MusShop.Domain.Model.RepositoryAbstractions.Base;
using MusShop.Domain.Model.ResultItems;
using MusShop.Domain.Model.ResultItems.ErrorsDocumentation;

namespace MusShop.Application.UseCases.Features.Blog.Categories.Commands.UpdateCategoryCommand;

public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, DomainResult<CategoryActionDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<DomainResult<CategoryActionDto>> Handle(UpdateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        if (request.CategoryActionDto.CategoryName.Length is 0 or > 100)
        {
            return DomainResult<CategoryActionDto>.Failure(BlogErrors.CategoryNameError);
        }
        
        Category? category = await _unitOfWork.GetRepository<Category>().GetById(request.CategoryId);

        if (category is null)
        {
            return DomainResult<CategoryActionDto>.Failure(BlogErrors.CategoryNotFound);
        }

        _mapper.Map(request.CategoryActionDto, category);
        
        _unitOfWork.GetRepository<Category>().Update(category);
        await _unitOfWork.CommitAsync(cancellationToken);

        return DomainResult<CategoryActionDto>.Success(request.CategoryActionDto);
    }
}