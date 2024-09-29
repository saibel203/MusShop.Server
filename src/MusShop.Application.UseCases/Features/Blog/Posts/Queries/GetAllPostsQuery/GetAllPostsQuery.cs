using MediatR;
using MusShop.Application.Dtos.Blog.Post;
using MusShop.Contracts.Filters;
using MusShop.Contracts.Responses;
using MusShop.Domain.Model.ResultItems;

namespace MusShop.Application.UseCases.Features.Blog.Posts.Queries.GetAllPostsQuery;

//ublic record GetAllPostsQuery : IRequest<DomainResult<IEnumerable<PostDto>>>;
public record GetAllPostsQuery(PostFilter? Filter) : IRequest<DomainResult<PaginatedList<PostDto>>>;
