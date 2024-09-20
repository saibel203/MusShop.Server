using System.Net;
using MusShop.Application.Dtos.Blog.Post;
using MusShop.Application.UseCases.Commons.Behaviors;
using MusShop.Application.UseCases.Features.Blog.Posts.Commands.UpdatePostCommand;
using MusShop.Domain.Model.Entities.Blog;
using MusShop.Domain.Model.ResultItems.ErrorsDocumentation;
using NSubstitute.ReturnsExtensions;

namespace MusShop.Application.UseCases.UnitTests.Blog.Posts.Commands;

public class UpdatePostCommandTests : BaseUnitTest
{
    [Theory, AutoNSubstituteData]
    public async Task UpdatePost_EmptyTitle_ReturnErrorResponse(
        PostActionDto postActionDto, Guid id)
    {
        // Arrange
        UpdatePostHandler commandHandler = new UpdatePostHandler(UnitOfWork, Mapper);
        postActionDto.Title = string.Empty;

        ValidationPipelineBehavior<UpdatePostCommand, DomainResult<PostDto>> pipeline =
            GetValidationPipeline<UpdatePostCommand, PostDto>(
                BlogErrors.PostTitleRequiredError.Description, nameof(postActionDto.Title));

        UpdatePostCommand invalidCommand = new UpdatePostCommand(postActionDto, id)
        {
            PostActionDto = new PostActionDto { Title = string.Empty }
        };

        Task<DomainResult<PostDto>> Next() =>
            commandHandler.Handle(invalidCommand, CancellationToken.None);

        // Act
        DomainResult<PostDto> result =
            await pipeline.Handle(invalidCommand, Next, CancellationToken.None);

        // Assert
        result.ShouldBeAssignableTo<DomainResult<PostDto>>();
        result.IsSuccess.ShouldBeFalse();
        result.IsFailure.ShouldBeTrue();
        result.Error.Description.ShouldBeEmpty();
        result.Error.Code.ShouldBeEmpty();
        result.Errors.ShouldNotBeEmpty();
        result.Errors[0].Description.ShouldBe(BlogErrors.PostTitleRequiredError.Description);

        await UnitOfWork.DidNotReceive().CommitAsync(Arg.Any<CancellationToken>());
    }

    [Theory, AutoNSubstituteData]
    public async Task UpdatePost_MaxLengthTitle_ReturnErrorResponse(
        PostActionDto postActionDto, Guid id)
    {
        // Arrange
        UpdatePostHandler commandHandler = new UpdatePostHandler(UnitOfWork, Mapper);
        postActionDto.Title = GenerateCustomLengthString(201);

        ValidationPipelineBehavior<UpdatePostCommand, DomainResult<PostDto>> pipeline =
            GetValidationPipeline<UpdatePostCommand, PostDto>(
                BlogErrors.PostTitleLengthError.Description, nameof(postActionDto.Title));

        UpdatePostCommand invalidCommand = new UpdatePostCommand(postActionDto, id)
        {
            PostActionDto = new PostActionDto { Title = GenerateCustomLengthString(201) }
        };

        Task<DomainResult<PostDto>> Next() =>
            commandHandler.Handle(invalidCommand, CancellationToken.None);

        // Act
        DomainResult<PostDto> result =
            await pipeline.Handle(invalidCommand, Next, CancellationToken.None);

        // Assert
        result.ShouldBeAssignableTo<DomainResult<PostDto>>();
        result.IsSuccess.ShouldBeFalse();
        result.IsFailure.ShouldBeTrue();
        result.Error.Description.ShouldBeEmpty();
        result.Error.Code.ShouldBeEmpty();
        result.Errors.ShouldNotBeEmpty();
        result.Errors[0].Description.ShouldBe(BlogErrors.PostTitleLengthError.Description);

        await UnitOfWork.DidNotReceive().CommitAsync(Arg.Any<CancellationToken>());
    }

    [Theory, AutoNSubstituteData]
    public async Task UpdatePost_EmptyDescription_ReturnErrorResponse(
        PostActionDto postActionDto, Guid id)
    {
        // Arrange
        UpdatePostHandler commandHandler = new UpdatePostHandler(UnitOfWork, Mapper);
        postActionDto.Description = string.Empty;

        ValidationPipelineBehavior<UpdatePostCommand, DomainResult<PostDto>> pipeline =
            GetValidationPipeline<UpdatePostCommand, PostDto>(
                BlogErrors.PostDescriptionRequiredError.Description, nameof(postActionDto.Title));

        UpdatePostCommand invalidCommand = new UpdatePostCommand(postActionDto, id)
        {
            PostActionDto = new PostActionDto { Description = string.Empty }
        };

        Task<DomainResult<PostDto>> Next() =>
            commandHandler.Handle(invalidCommand, CancellationToken.None);

        // Act
        DomainResult<PostDto> result =
            await pipeline.Handle(invalidCommand, Next, CancellationToken.None);

        // Assert
        result.ShouldBeAssignableTo<DomainResult<PostDto>>();
        result.IsSuccess.ShouldBeFalse();
        result.IsFailure.ShouldBeTrue();
        result.Error.Description.ShouldBeEmpty();
        result.Error.Code.ShouldBeEmpty();
        result.Errors.ShouldNotBeEmpty();
        result.Errors[0].Description.ShouldBe(BlogErrors.PostDescriptionRequiredError.Description);

        await UnitOfWork.DidNotReceive().CommitAsync(Arg.Any<CancellationToken>());
    }
    
    [Theory, AutoNSubstituteData]
    public async Task UpdatePost_MaxLengthImageUrl_ReturnErrorResponse(
        PostActionDto postActionDto, Guid id)
    {
        // Arrange
        UpdatePostHandler commandHandler = new UpdatePostHandler(UnitOfWork, Mapper);
        postActionDto.ImageUrl = GenerateCustomLengthString(1001);

        ValidationPipelineBehavior<UpdatePostCommand, DomainResult<PostDto>> pipeline =
            GetValidationPipeline<UpdatePostCommand, PostDto>(
                BlogErrors.PostImageLengthError.Description, nameof(postActionDto.Title));

        UpdatePostCommand invalidCommand = new UpdatePostCommand(postActionDto, id)
        {
            PostActionDto = new PostActionDto { ImageUrl = GenerateCustomLengthString(1001) }
        };

        Task<DomainResult<PostDto>> Next() =>
            commandHandler.Handle(invalidCommand, CancellationToken.None);

        // Act
        DomainResult<PostDto> result =
            await pipeline.Handle(invalidCommand, Next, CancellationToken.None);

        // Assert
        result.ShouldBeAssignableTo<DomainResult<PostDto>>();
        result.IsSuccess.ShouldBeFalse();
        result.IsFailure.ShouldBeTrue();
        result.Error.Description.ShouldBeEmpty();
        result.Error.Code.ShouldBeEmpty();
        result.Errors.ShouldNotBeEmpty();
        result.Errors[0].Description.ShouldBe(BlogErrors.PostImageLengthError.Description);

        await UnitOfWork.DidNotReceive().CommitAsync(Arg.Any<CancellationToken>());
    }
    
    [Theory, AutoNSubstituteData]
    public async Task UpdatePost_EmptyCategory_ReturnErrorResponse(
        PostActionDto postActionDto, Guid id)
    {
        // Arrange
        UpdatePostHandler commandHandler = new UpdatePostHandler(UnitOfWork, Mapper);
        postActionDto.CategoryId = Guid.Empty;

        ValidationPipelineBehavior<UpdatePostCommand, DomainResult<PostDto>> pipeline =
            GetValidationPipeline<UpdatePostCommand, PostDto>(
                BlogErrors.PostCategoryRequiredError.Description, nameof(postActionDto.Title));

        UpdatePostCommand invalidCommand = new UpdatePostCommand(postActionDto, id)
        {
            PostActionDto = new PostActionDto { CategoryId = Guid.Empty }
        };

        Task<DomainResult<PostDto>> Next() =>
            commandHandler.Handle(invalidCommand, CancellationToken.None);

        // Act
        DomainResult<PostDto> result =
            await pipeline.Handle(invalidCommand, Next, CancellationToken.None);

        // Assert
        result.ShouldBeAssignableTo<DomainResult<PostDto>>();
        result.IsSuccess.ShouldBeFalse();
        result.IsFailure.ShouldBeTrue();
        result.Error.Description.ShouldBeEmpty();
        result.Error.Code.ShouldBeEmpty();
        result.Errors.ShouldNotBeEmpty();
        result.Errors[0].Description.ShouldBe(BlogErrors.PostCategoryRequiredError.Description);

        await UnitOfWork.DidNotReceive().CommitAsync(Arg.Any<CancellationToken>());
    }
    
    [Theory, AutoNSubstituteData]
    public async Task UpdatePost_NonExistingPostWithId_ReturnErrorResponse(
        PostActionDto postActionDto, Guid id)
    {
        // Arrange
        UpdatePostHandler commandHandler = new UpdatePostHandler(UnitOfWork, Mapper);

        UnitOfWork.GetRepository<Post>().GetById(id).ReturnsNull();

        // Act
        DomainResult<PostDto> updatePostResult =
            await commandHandler.Handle(new UpdatePostCommand(postActionDto, id), CancellationToken.None);

        // Assert
        updatePostResult.ShouldBeAssignableTo<DomainResult<PostDto>>();
        updatePostResult.IsSuccess.ShouldBeFalse();
        updatePostResult.IsFailure.ShouldBeTrue();
        updatePostResult.Error.Description.ShouldBe(BlogErrors.PostNotFound.Description);
        updatePostResult.Error.Code.ShouldBe(BlogErrors.PostNotFound.Code);
        updatePostResult.Error.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        updatePostResult.Errors.ShouldBeEmpty();
    }

    [Theory, AutoNSubstituteData]
    public async Task UpdatePost_ValidData_ReturnNewPostResponse(
        PostActionDto postActionDto, Guid id, Post post)
    {
        // Arrange
        UpdatePostHandler commandHandler = new UpdatePostHandler(UnitOfWork, Mapper);

        UnitOfWork.GetRepository<Post>().GetById(id).Returns(post);

        // Act
        DomainResult<PostDto> updatePostResult =
            await commandHandler.Handle(new UpdatePostCommand(postActionDto, id), CancellationToken.None);

        // Assert
        updatePostResult.ShouldBeAssignableTo<DomainResult<PostDto>>();
        updatePostResult.IsSuccess.ShouldBeTrue();
        updatePostResult.IsFailure.ShouldBeFalse();
        updatePostResult.Error.Description.ShouldBeEmpty();
        updatePostResult.Error.Code.ShouldBeEmpty();
        updatePostResult.Errors.ShouldBeEmpty();
        updatePostResult.Value.ShouldNotBeNull();

        UnitOfWork.GetRepository<Post>().Received(BaseReceivedCount).Update(Arg.Any<Post>());
        await UnitOfWork.Received(BaseReceivedCount).CommitAsync(CancellationToken.None);
    }
}