using MusShop.Application.Dtos.Blog.Post;
using MusShop.Application.UseCases.Commons.Behaviors;
using MusShop.Application.UseCases.Features.Blog.Posts.Commands.CreatePostCommand;
using MusShop.Domain.Model.Entities.Blog;
using MusShop.Domain.Model.ResultItems.ErrorsDocumentation;

namespace MusShop.Application.UseCases.UnitTests.Blog.Posts.Commands;

public class CreatePostCommandTests : BaseUnitTest
{
    [Theory, AutoNSubstituteData]
    public async Task CreatePost_EmptyTitle_ReturnErrorResponse(
        PostActionDto postActionDto)
    {
        // Arrange
        CreatePostHandler commandHandler = new CreatePostHandler(UnitOfWork, Mapper);
        postActionDto.Title = string.Empty;

        ValidationPipelineBehavior<CreatePostCommand, DomainResult<PostDto>> pipeline =
            GetValidationPipeline<CreatePostCommand, PostDto>(
                BlogErrors.PostTitleRequiredError.Description, nameof(postActionDto.Title));

        CreatePostCommand invalidCommand = new CreatePostCommand(postActionDto)
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
    public async Task CreatePost_MaxLengthTitle_ReturnErrorResponse(
        PostActionDto postActionDto)
    {
        // Arrange
        CreatePostHandler commandHandler = new CreatePostHandler(UnitOfWork, Mapper);
        postActionDto.Title = GenerateCustomLengthString(201);

        ValidationPipelineBehavior<CreatePostCommand, DomainResult<PostDto>> pipeline =
            GetValidationPipeline<CreatePostCommand, PostDto>(
                BlogErrors.PostTitleLengthError.Description, nameof(postActionDto.Title));

        CreatePostCommand invalidCommand = new CreatePostCommand(postActionDto)
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
    public async Task CreatePost_EmptyDescription_ReturnErrorResponse(
        PostActionDto postActionDto)
    {
        // Arrange
        CreatePostHandler commandHandler = new CreatePostHandler(UnitOfWork, Mapper);
        postActionDto.Description = string.Empty;

        ValidationPipelineBehavior<CreatePostCommand, DomainResult<PostDto>> pipeline =
            GetValidationPipeline<CreatePostCommand, PostDto>(
                BlogErrors.PostDescriptionRequiredError.Description, nameof(postActionDto.Title));

        CreatePostCommand invalidCommand = new CreatePostCommand(postActionDto)
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
    public async Task CreatePost_MaxLengthImage_ReturnErrorResponse(
        PostActionDto postActionDto)
    {
        // Arrange
        CreatePostHandler commandHandler = new CreatePostHandler(UnitOfWork, Mapper);
        postActionDto.ImageUrl = GenerateCustomLengthString(1001);

        ValidationPipelineBehavior<CreatePostCommand, DomainResult<PostDto>> pipeline =
            GetValidationPipeline<CreatePostCommand, PostDto>(
                BlogErrors.PostImageLengthError.Description, nameof(postActionDto.Title));

        CreatePostCommand invalidCommand = new CreatePostCommand(postActionDto)
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
    public async Task CreatePost_EmptyCategory_ReturnErrorResponse(
        PostActionDto postActionDto)
    {
        // Arrange
        CreatePostHandler commandHandler = new CreatePostHandler(UnitOfWork, Mapper);
        postActionDto.CategoryId = Guid.Empty;

        ValidationPipelineBehavior<CreatePostCommand, DomainResult<PostDto>> pipeline =
            GetValidationPipeline<CreatePostCommand, PostDto>(
                BlogErrors.PostCategoryRequiredError.Description, nameof(postActionDto.Title));

        CreatePostCommand invalidCommand = new CreatePostCommand(postActionDto)
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
    public async Task CreatePost_ValidData_ReturnNewPostResponse(
        PostActionDto postActionDto)
    {
        // Arrange
        CreatePostHandler commandHandler = new CreatePostHandler(UnitOfWork, Mapper);

        // Act
        DomainResult<PostDto> createPostResult =
            await commandHandler.Handle(new CreatePostCommand(postActionDto), CancellationToken.None);

        // Assert
        createPostResult.ShouldBeAssignableTo<DomainResult<PostDto>>();
        createPostResult.IsSuccess.ShouldBeTrue();
        createPostResult.IsFailure.ShouldBeFalse();
        createPostResult.Error.Description.ShouldBeEmpty();
        createPostResult.Error.Code.ShouldBeEmpty();
        createPostResult.Errors.ShouldBeEmpty();

        await UnitOfWork.GetRepository<Post>().Received(BaseReceivedCount).Add(Arg.Any<Post>());
        await UnitOfWork.Received(BaseReceivedCount).CommitAsync(CancellationToken.None);
    }
}