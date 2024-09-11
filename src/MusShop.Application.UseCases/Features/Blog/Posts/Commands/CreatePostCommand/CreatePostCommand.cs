using MediatR;
using MusShop.Application.Dtos.Blog.Post;
using MusShop.Domain.Model.ResultItems;

namespace MusShop.Application.UseCases.Features.Blog.Posts.Commands.CreatePostCommand;

public record CreatePostCommand(PostActionDto PostActionDto) : IRequest<DomainResult<PostDto>>;