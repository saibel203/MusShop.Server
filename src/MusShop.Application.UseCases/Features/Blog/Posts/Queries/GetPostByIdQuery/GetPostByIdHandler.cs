using MapsterMapper;
using MediatR;
using MusShop.Application.Dtos.Blog.Post;
using MusShop.Application.UseCases.Commons;
using MusShop.Contracts.Filters;
using MusShop.Contracts.RepositoryAbstractions.Base;
using MusShop.Domain.Model.Entities.Blog;
using MusShop.Domain.Model.ResultItems;
using MusShop.Domain.Model.ResultItems.ErrorsDocumentation;

namespace MusShop.Application.UseCases.Features.Blog.Posts.Queries.GetPostByIdQuery;

public class GetPostByIdHandler : BaseFeatureConfigs, IRequestHandler<GetPostByIdQuery, DomainResult<PostDto>>
{
    public GetPostByIdHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<DomainResult<PostDto>> Handle(GetPostByIdQuery request,
        CancellationToken cancellationToken)
    {
        Post? post = await UnitOfWork.GetRepository<Post, PostFilter>().GetById(request.Id);

        if (post is null)
        {
            return DomainResult<PostDto>.Failure(BlogErrors.PostNotFound);
        }

        PostDto postDto = Mapper.Map<PostDto>(post);

        return DomainResult<PostDto>.Success(postDto);
    }
}