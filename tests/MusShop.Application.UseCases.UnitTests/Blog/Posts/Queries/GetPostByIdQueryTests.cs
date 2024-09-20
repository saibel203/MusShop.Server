using System.Net;
using MusShop.Application.Dtos.Blog.Post;
using MusShop.Application.UseCases.Features.Blog.Posts.Queries.GetPostByIdQuery;
using MusShop.Domain.Model.Entities.Blog;
using MusShop.Domain.Model.ResultItems.ErrorsDocumentation;
using NSubstitute.ReturnsExtensions;

namespace MusShop.Application.UseCases.UnitTests.Blog.Posts.Queries;

public class GetPostByIdQueryTests : BaseUnitTest
{
    [Theory, AutoNSubstituteData]
    public async Task GetPostById_NonExistingPostWithId_ReturnErrorResponse(
        Guid postId)
    {
        // Arrange
        GetPostByIdHandler queryHandler = new GetPostByIdHandler(UnitOfWork, Mapper);

        UnitOfWork.GetRepository<Post>().GetById(postId).ReturnsNull();

        // Act
        DomainResult<PostDto> getPostByIdResult =
            await queryHandler.Handle(new GetPostByIdQuery(postId), CancellationToken.None);

        // Assert
        getPostByIdResult.ShouldBeAssignableTo<DomainResult<PostDto>>();
        getPostByIdResult.IsSuccess.ShouldBeFalse();
        getPostByIdResult.IsFailure.ShouldBeTrue();
        getPostByIdResult.Error.Description.ShouldBe(BlogErrors.PostNotFound.Description);
        getPostByIdResult.Error.Code.ShouldBe(BlogErrors.PostNotFound.Code);
        getPostByIdResult.Error.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        getPostByIdResult.Errors.ShouldBeEmpty();
    }
    
    [Theory, AutoNSubstituteData]
    public async Task GetPostById_ExistingPostWithId_ReturnPostResponse(
        Guid postId, Post post)
    {
        // Arrange
        GetPostByIdHandler queryHandler = new GetPostByIdHandler(UnitOfWork, Mapper);

        UnitOfWork.GetRepository<Post>().GetById(postId).Returns(post);

        // Act
        DomainResult<PostDto> getPostByIdResult =
            await queryHandler.Handle(new GetPostByIdQuery(postId), CancellationToken.None);

        // Assert
        getPostByIdResult.ShouldBeAssignableTo<DomainResult<PostDto>>();
        getPostByIdResult.IsSuccess.ShouldBeTrue();
        getPostByIdResult.IsFailure.ShouldBeFalse();
        getPostByIdResult.Error.Description.ShouldBeEmpty();
        getPostByIdResult.Error.Code.ShouldBeEmpty();
        getPostByIdResult.Errors.ShouldBeEmpty();
        getPostByIdResult.Value.ShouldNotBeNull();
    }
}