using MediatR;
using MusShop.Application.Dtos.Blog.Post;
using MusShop.Domain.Model.ResultItems;

namespace MusShop.Application.UseCases.Features.Blog.Posts.Queries.GetAllPostsQuery;

public record GetAllPostsQuery : IRequest<DomainResult<IEnumerable<PostDto>>>;