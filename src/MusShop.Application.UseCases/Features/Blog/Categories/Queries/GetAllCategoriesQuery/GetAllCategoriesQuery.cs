using MediatR;
using MusShop.Application.Dtos.Blog.Category;
using MusShop.Contracts.Responses;
using MusShop.Domain.Model.ResultItems;

namespace MusShop.Application.UseCases.Features.Blog.Categories.Queries.GetAllCategoriesQuery;

public record GetAllCategoriesQuery : IRequest<DomainResult<IEnumerable<CategoryDto>>>;