using MapsterMapper;
using MediatR;
using MusShop.Application.Dtos.Blog.Category;
using MusShop.Domain.Model.Entities.Blog;
using MusShop.Domain.Model.RepositoryAbstractions.Base;
using MusShop.Domain.Model.ResultItems;
using MusShop.Domain.Model.ResultItems.ErrorsDocumentation;

namespace MusShop.Application.UseCases.Features.Blog.Categories.Commands.CreateCategoryCommand;

public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, DomainResult<CategoryDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<DomainResult<CategoryDto>> Handle(CreateCategoryCommand request, 
        CancellationToken cancellationToken)
    {
        if (request.CategoryActionDto.CategoryName.Length is 0 or > 100)
        {
            return DomainResult<CategoryDto>.Failure(BlogErrors.CategoryNameError);
        }
        
        Category createdCategory = _mapper.Map<Category>(request.CategoryActionDto);
        
        Category newCategory = 
            await _unitOfWork.GetRepository<Category>().Add(createdCategory);
        await _unitOfWork.CommitAsync(cancellationToken);

        CategoryDto newCategoryDto = _mapper.Map<CategoryDto>(newCategory);

        return DomainResult<CategoryDto>.Success(newCategoryDto);
    }
}