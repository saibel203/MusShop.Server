using MapsterMapper;
using MediatR;
using MusShop.Application.Dtos.Blog.Post;
using MusShop.Application.UseCases.Commons;
using MusShop.Domain.Model.Entities.Blog;
using MusShop.Domain.Model.RepositoryAbstractions.Base;
using MusShop.Domain.Model.ResultItems;

namespace MusShop.Application.UseCases.Features.Blog.Posts.Commands.CreatePostCommand;

public class CreatePostHandler : BaseFeatureConfigs, IRequestHandler<CreatePostCommand, DomainResult<PostDto>>
{
    public CreatePostHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<DomainResult<PostDto>> Handle(CreatePostCommand request,
        CancellationToken cancellationToken)
    {
        Post post = Mapper.Map<Post>(request.PostActionDto);

        Post newPost = await UnitOfWork.GetRepository<Post>().Add(post);
        await UnitOfWork.CommitAsync(cancellationToken);

        PostDto postDto = Mapper.Map<PostDto>(newPost);

        return DomainResult<PostDto>.Success(postDto);
    }
}