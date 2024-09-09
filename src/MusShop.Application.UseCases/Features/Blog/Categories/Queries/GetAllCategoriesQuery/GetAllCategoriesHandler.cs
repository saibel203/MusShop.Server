using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MusShop.Application.Dtos.Blog;
using MusShop.Persistence;

namespace MusShop.Application.UseCases.Features.Blog.Categories.Queries.GetAllCategoriesQuery;

public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryDto>>
{
    private readonly MusShopDataDbContext _context;
    private readonly IMapper _mapper;

    public GetAllCategoriesHandler(MusShopDataDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryDto>> Handle(GetAllCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var categories = await _context.Categories.ToListAsync(cancellationToken);
        var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);

        return categoriesDto;
    }
}