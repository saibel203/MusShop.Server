using MusShop.Application.Dtos.Blog.Post;
using MusShop.Application.UseCases.Features.Blog.Posts.Queries.GetAllPostsQuery;
using MusShop.Domain.Model.Entities.Blog;

namespace MusShop.Application.UseCases.UnitTests.Blog.Posts.Queries;

public class GetAllPostsQueryTests : BaseUnitTest
{
    [Theory, AutoNSubstituteData]
    public async Task GetAllPosts_NotEmptyResultList_ReturnPostsList(
        IReadOnlyList<Post> expectedPosts)
    {
        // Arrange
        GetAllPostsHandler queryHandler =
            new GetAllPostsHandler(UnitOfWork, Mapper);
        int expectedResultCount = expectedPosts.Count;
        
        UnitOfWork.GetRepository<Post>().GetAll().Returns(expectedPosts.AsEnumerable());
        
        // Act
        DomainResult<IEnumerable<PostDto>> getAllPostsResult =
            await queryHandler.Handle(new GetAllPostsQuery(), CancellationToken.None);

        // Assert
        getAllPostsResult.ShouldBeAssignableTo<DomainResult<IEnumerable<PostDto>>>();
        getAllPostsResult.IsSuccess.ShouldBeTrue();
        getAllPostsResult.IsFailure.ShouldBeFalse();
        getAllPostsResult.Error.Code.ShouldBeEmpty();
        getAllPostsResult.Error.Description.ShouldBeEmpty();
        getAllPostsResult.Errors.ShouldBeEmpty();
        getAllPostsResult.Value.ShouldNotBeEmpty();
        getAllPostsResult.Value.Count().ShouldBe(expectedResultCount);
    }
    
    [Fact]
    public async Task GetAllPosts_EmptyResultList_ReturnEmptyList()
    {
        // Arrange
        GetAllPostsHandler queryHandler =
            new GetAllPostsHandler(UnitOfWork, Mapper);
        
        UnitOfWork.GetRepository<Post>().GetAll().Returns(Enumerable.Empty<Post>());
        
        // Act
        DomainResult<IEnumerable<PostDto>> getAllPostsResult =
            await queryHandler.Handle(new GetAllPostsQuery(), CancellationToken.None);

        // Assert
        getAllPostsResult.ShouldBeAssignableTo<DomainResult<IEnumerable<PostDto>>>();
        getAllPostsResult.IsSuccess.ShouldBeTrue();
        getAllPostsResult.IsFailure.ShouldBeFalse();
        getAllPostsResult.Error.Code.ShouldBeEmpty();
        getAllPostsResult.Error.Description.ShouldBeEmpty();
        getAllPostsResult.Errors.ShouldBeEmpty();
        getAllPostsResult.Value.ShouldBeEmpty();
    }
}