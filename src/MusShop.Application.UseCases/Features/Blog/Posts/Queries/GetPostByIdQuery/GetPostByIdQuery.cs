using MediatR;
using MusShop.Application.Dtos.Blog.Post;
using MusShop.Domain.Model.ResultItems;

namespace MusShop.Application.UseCases.Features.Blog.Posts.Queries.GetPostByIdQuery;

public record GetPostByIdQuery(Guid Id) : IRequest<DomainResult<PostDto>>;