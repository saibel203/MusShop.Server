using MapsterMapper;
using MediatR;
using MusShop.Application.Dtos.Blog;
using MusShop.Domain.Model.Entities.Blog;
using MusShop.Domain.Model.RepositoryAbstractions.Base;

namespace MusShop.Application.UseCases.Features.Blog.Categories.Queries.GetAllCategoriesQuery;

public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllCategoriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryDto>> Handle(GetAllCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var categories = await _unitOfWork.Repository().GetAll<Category>();
        var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);

        return categoriesDto;
    }
}