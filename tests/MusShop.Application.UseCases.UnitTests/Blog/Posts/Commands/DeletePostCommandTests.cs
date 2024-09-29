using System.Net;
using MusShop.Application.Dtos.Blog.Post;
using MusShop.Application.UseCases.Features.Blog.Posts.Commands.DeletePostCommand;
using MusShop.Contracts.Filters;
using MusShop.Domain.Model.Entities.Blog;
using MusShop.Domain.Model.ResultItems.ErrorsDocumentation;
using NSubstitute.ReturnsExtensions;

namespace MusShop.Application.UseCases.UnitTests.Blog.Posts.Commands;

public class DeletePostCommandTests : BaseUnitTest
{
    [Theory, AutoNSubstituteData]
    public async Task DeletePost_NonExistingPostWithId_ReturnErrorResponse(
        Guid postId)
    {
        // Arrange
        DeletePostHandler commandHandler = new DeletePostHandler(UnitOfWork, Mapper);

        UnitOfWork.GetRepository<Post, PostFilter>().GetById(postId).ReturnsNull();

        // Act
        DomainResult<PostDto> deletePostResult =
            await commandHandler.Handle(new DeletePostCommand(postId), CancellationToken.None);

        // Assert
        deletePostResult.ShouldBeAssignableTo<DomainResult<PostDto>>();
        deletePostResult.IsSuccess.ShouldBeFalse();
        deletePostResult.IsFailure.ShouldBeTrue();
        deletePostResult.Error.Description.ShouldBe(BlogErrors.PostNotFound.Description);
        deletePostResult.Error.Code.ShouldBe(BlogErrors.PostNotFound.Code);
        deletePostResult.Error.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
    
    [Theory, AutoNSubstituteData]
    public async Task DeletePost_ExistingPostWithId_ReturnPostResponse(
        Guid postId, Post post)
    {
        // Arrange
        DeletePostHandler commandHandler = new DeletePostHandler(UnitOfWork, Mapper);

        UnitOfWork.GetRepository<Post, PostFilter>().GetById(postId).Returns(post);

        // Act
        DomainResult<PostDto> deletePostResult =
            await commandHandler.Handle(new DeletePostCommand(postId), CancellationToken.None);

        // Assert
        deletePostResult.ShouldBeAssignableTo<DomainResult<PostDto>>();
        deletePostResult.IsSuccess.ShouldBeTrue();
        deletePostResult.IsFailure.ShouldBeFalse();
        deletePostResult.Error.Description.ShouldBeEmpty();
        deletePostResult.Error.Code.ShouldBeEmpty();
        deletePostResult.Errors.ShouldBeEmpty();
        
        UnitOfWork.GetRepository<Post, PostFilter>().Received(BaseReceivedCount).Delete(post);
        await UnitOfWork.Received(BaseReceivedCount).CommitAsync(CancellationToken.None);
    }
}