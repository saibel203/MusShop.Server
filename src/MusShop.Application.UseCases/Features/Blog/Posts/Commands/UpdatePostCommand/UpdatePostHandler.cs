using MapsterMapper;
using MediatR;
using MusShop.Application.Dtos.Blog.Post;
using MusShop.Application.UseCases.Commons;
using MusShop.Domain.Model.Entities.Blog;
using MusShop.Domain.Model.RepositoryAbstractions.Base;
using MusShop.Domain.Model.ResultItems;
using MusShop.Domain.Model.ResultItems.ErrorsDocumentation;

namespace MusShop.Application.UseCases.Features.Blog.Posts.Commands.UpdatePostCommand;

public class UpdatePostHandler : BaseFeatureConfigs, IRequestHandler<UpdatePostCommand, DomainResult<PostDto>>
{
    public UpdatePostHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<DomainResult<PostDto>> Handle(UpdatePostCommand request,
        CancellationToken cancellationToken)
    {
        Post? post = await UnitOfWork.GetRepository<Post>().GetById(request.Id);

        if (post is null)
        {
            return DomainResult<PostDto>.Failure(BlogErrors.PostNotFound);
        }

        Mapper.Map(request.PostActionDto, post);

        UnitOfWork.GetRepository<Post>().Update(post);
        await UnitOfWork.CommitAsync(cancellationToken);

        PostDto postDto = Mapper.Map<PostDto>(post);

        return DomainResult<PostDto>.Success(postDto);
    }
}