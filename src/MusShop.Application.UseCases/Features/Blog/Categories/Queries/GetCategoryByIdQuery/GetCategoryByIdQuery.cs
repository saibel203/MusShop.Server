using MediatR;
using MusShop.Application.Dtos.Blog.Category;
using MusShop.Domain.Model.ResultItems;

namespace MusShop.Application.UseCases.Features.Blog.Categories.Queries.GetCategoryByIdQuery;

public record GetCategoryByIdQuery(Guid Id) : IRequest<DomainResult<CategoryDto>>;