using Mapster;
using MapsterMapper;
using MediatR;
using MusShop.Application.Dtos.Blog.Post;
using MusShop.Application.UseCases.Commons;
using MusShop.Contracts.Filters;
using MusShop.Contracts.RepositoryAbstractions.Base;
using MusShop.Contracts.Responses;
using MusShop.Domain.Model.Entities.Blog;
using MusShop.Domain.Model.ResultItems;

namespace MusShop.Application.UseCases.Features.Blog.Posts.Queries.GetAllPostsQuery;

public class GetAllPostsHandler : BaseFeatureConfigs,
    IRequestHandler<GetAllPostsQuery, DomainResult<PaginatedList<PostDto>>>
{
    public GetAllPostsHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<DomainResult<PaginatedList<PostDto>>> Handle(GetAllPostsQuery request,
        CancellationToken cancellationToken)
    {
        PaginatedList<Post> posts = await UnitOfWork.GetRepository<Post, PostFilter>()
            .GetAll(request.Filter);
        PaginatedList<PostDto> postsDto = posts.Adapt<PaginatedList<PostDto>>();
        
        return DomainResult<PaginatedList<PostDto>>.Success(postsDto);
    }
}