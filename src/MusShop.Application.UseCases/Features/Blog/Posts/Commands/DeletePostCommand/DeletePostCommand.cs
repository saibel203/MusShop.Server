using MediatR;
using MusShop.Application.Dtos.Blog.Post;
using MusShop.Domain.Model.ResultItems;

namespace MusShop.Application.UseCases.Features.Blog.Posts.Commands.DeletePostCommand;

public record DeletePostCommand(Guid Id) : IRequest<DomainResult<PostDto>>;