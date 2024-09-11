using MediatR;
using MusShop.Application.Dtos.Blog.Post;
using MusShop.Domain.Model.ResultItems;

namespace MusShop.Application.UseCases.Features.Blog.Posts.Commands.UpdatePostCommand;

public record UpdatePostCommand(PostActionDto PostActionDto, Guid Id) : IRequest<DomainResult<PostDto>>;