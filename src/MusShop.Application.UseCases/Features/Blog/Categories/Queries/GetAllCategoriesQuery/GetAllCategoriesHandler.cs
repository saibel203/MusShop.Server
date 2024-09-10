using MapsterMapper;
using MediatR;
using MusShop.Application.Dtos.Blog.Category;
using MusShop.Domain.Model.Entities.Blog;
using MusShop.Domain.Model.RepositoryAbstractions.Base;
using MusShop.Domain.Model.ResultItems;

namespace MusShop.Application.UseCases.Features.Blog.Categories.Queries.GetAllCategoriesQuery;

public class GetAllCategoriesHandler 
    : IRequestHandler<GetAllCategoriesQuery, DomainResult<IEnumerable<CategoryDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllCategoriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<DomainResult<IEnumerable<CategoryDto>>> Handle(GetAllCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        IEnumerable<Category> categories =
            await _unitOfWork.GetRepository<Category>().GetAll();
        IEnumerable<CategoryDto> categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);

        return DomainResult<IEnumerable<CategoryDto>>.Success(categoriesDto);
    }
}