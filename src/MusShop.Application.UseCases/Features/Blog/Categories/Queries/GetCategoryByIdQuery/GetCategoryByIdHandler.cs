using MapsterMapper;
using MediatR;
using MusShop.Application.Dtos.Blog.Category;
using MusShop.Domain.Model.Entities.Blog;
using MusShop.Domain.Model.RepositoryAbstractions.Base;
using MusShop.Domain.Model.ResultItems;
using MusShop.Domain.Model.ResultItems.ErrorsDocumentation;

namespace MusShop.Application.UseCases.Features.Blog.Categories.Queries.GetCategoryByIdQuery;

public class GetCategoryByIdHandler
    : IRequestHandler<GetCategoryByIdQuery, DomainResult<CategoryDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCategoryByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<DomainResult<CategoryDto>> Handle(GetCategoryByIdQuery request,
        CancellationToken cancellationToken)
    {
        Category? category = await _unitOfWork.GetRepository<Category>().GetById(request.Id);

        if (category is null)
        {
            return DomainResult<CategoryDto>.Failure(BlogErrors.CategoryNotFound);
        }

        CategoryDto categoryDto = _mapper.Map<CategoryDto>(category);

        return DomainResult<CategoryDto>.Success(categoryDto);
    }
}