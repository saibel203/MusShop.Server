using MapsterMapper;
using MediatR;
using MusShop.Application.Dtos.Blog.Post;
using MusShop.Application.UseCases.Commons;
using MusShop.Domain.Model.Entities.Blog;
using MusShop.Domain.Model.RepositoryAbstractions.Base;
using MusShop.Domain.Model.ResultItems;

namespace MusShop.Application.UseCases.Features.Blog.Posts.Queries.GetAllPostsQuery;

public class GetAllPostsHandler : BaseFeatureConfigs,
    IRequestHandler<GetAllPostsQuery, DomainResult<IEnumerable<PostDto>>>
{
    public GetAllPostsHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<DomainResult<IEnumerable<PostDto>>> Handle(GetAllPostsQuery request,
        CancellationToken cancellationToken)
    {
        IEnumerable<Post> posts = await UnitOfWork.GetRepository<Post>().GetAll();
        IEnumerable<PostDto> postsDto = Mapper.Map<IEnumerable<PostDto>>(posts);

        return DomainResult<IEnumerable<PostDto>>.Success(postsDto);
    }
}