using MediatR;
using MusShop.Application.Dtos.Blog;

namespace MusShop.Application.UseCases.Features.Blog.Categories.Queries.GetAllCategoriesQuery;

public record GetAllCategoriesQuery : IRequest<IEnumerable<CategoryDto>>;