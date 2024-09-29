using MusShop.Application.Dtos.Blog.Category;
using MusShop.Application.UseCases.Commons.Behaviors;
using MusShop.Application.UseCases.Features.Blog.Categories.Commands.CreateCategoryCommand;
using MusShop.Contracts.Filters;
using MusShop.Domain.Model.Entities.Blog;
using MusShop.Domain.Model.ResultItems.ErrorsDocumentation;

namespace MusShop.Application.UseCases.UnitTests.Blog.Categories.Commands;

public class CreateCategoryCommandTests : BaseUnitTest
{
    [Theory, AutoNSubstituteData]
    public async Task CreateCategory_EmptyName_ReturnErrorResponse(
        CategoryActionDto categoryActionDto)
    {
        // Arrange
        CreateCategoryHandler commandHandler = new CreateCategoryHandler(UnitOfWork, Mapper);
        categoryActionDto.CategoryName = string.Empty;

        ValidationPipelineBehavior<CreateCategoryCommand, DomainResult<CategoryDto>> pipeline =
            GetValidationPipeline<CreateCategoryCommand, CategoryDto>(
                BlogErrors.CategoryNameRequiredError.Description, nameof(categoryActionDto.CategoryName));

        CreateCategoryCommand invalidCommand = new CreateCategoryCommand(categoryActionDto)
        {
            CategoryActionDto = new CategoryActionDto { CategoryName = string.Empty }
        };

        Task<DomainResult<CategoryDto>> Next() =>
            commandHandler.Handle(invalidCommand, CancellationToken.None);

        // Act
        DomainResult<CategoryDto> result =
            await pipeline.Handle(invalidCommand, Next, CancellationToken.None);

        // Assert
        result.ShouldBeAssignableTo<DomainResult<CategoryDto>>();
        result.IsSuccess.ShouldBeFalse();
        result.IsFailure.ShouldBeTrue();
        result.Error.Description.ShouldBeEmpty();
        result.Error.Code.ShouldBeEmpty();
        result.Errors.ShouldNotBeEmpty();
        result.Errors[0].Description.ShouldBe(BlogErrors.CategoryNameRequiredError.Description);

        await UnitOfWork.DidNotReceive().CommitAsync(Arg.Any<CancellationToken>());
    }
    
    [Theory, AutoNSubstituteData]
    public async Task CreateCategory_BadName_ReturnErrorResponse(
        CategoryActionDto categoryActionDto)
    {
        // Arrange
        CreateCategoryHandler commandHandler = new CreateCategoryHandler(UnitOfWork, Mapper);
        categoryActionDto.CategoryName = GenerateCustomLengthString(101);

        ValidationPipelineBehavior<CreateCategoryCommand, DomainResult<CategoryDto>> pipeline =
            GetValidationPipeline<CreateCategoryCommand, CategoryDto>(
                BlogErrors.CategoryNameLengthError.Description, nameof(categoryActionDto.CategoryName));

        CreateCategoryCommand invalidCommand = new CreateCategoryCommand(categoryActionDto)
        {
            CategoryActionDto = new CategoryActionDto { CategoryName = GenerateCustomLengthString(101) }
        };

        Task<DomainResult<CategoryDto>> Next() =>
            commandHandler.Handle(invalidCommand, CancellationToken.None);

        // Act
        DomainResult<CategoryDto> result =
            await pipeline.Handle(invalidCommand, Next, CancellationToken.None);

        // Assert
        result.ShouldBeAssignableTo<DomainResult<CategoryDto>>();
        result.IsSuccess.ShouldBeFalse();
        result.IsFailure.ShouldBeTrue();
        result.Error.Description.ShouldBeEmpty();
        result.Error.Code.ShouldBeEmpty();
        result.Errors.ShouldNotBeEmpty();
        result.Errors[0].Description.ShouldBe(BlogErrors.CategoryNameLengthError.Description);

        await UnitOfWork.DidNotReceive().CommitAsync(Arg.Any<CancellationToken>());
    }

    [Theory, AutoNSubstituteData]
    public async Task CreateCategory_ValidData_ReturnNewCategoryResponse(
        CategoryActionDto categoryActionDto)
    {
        // Arrange
        CreateCategoryHandler commandHandler = new CreateCategoryHandler(UnitOfWork, Mapper);

        // Act
        DomainResult<CategoryDto> createPostResult =
            await commandHandler.Handle(new CreateCategoryCommand(categoryActionDto), CancellationToken.None);

        // Assert
        createPostResult.ShouldBeAssignableTo<DomainResult<CategoryDto>>();
        createPostResult.IsSuccess.ShouldBeTrue();
        createPostResult.IsFailure.ShouldBeFalse();
        createPostResult.Error.Description.ShouldBeEmpty();
        createPostResult.Error.Code.ShouldBeEmpty();
        createPostResult.Errors.ShouldBeEmpty();
        createPostResult.Value.ShouldNotBeNull();

        await UnitOfWork.GetRepository<Category, CategoryFilter>().Received(BaseReceivedCount).Add(Arg.Any<Category>());
        await UnitOfWork.Received(BaseReceivedCount).CommitAsync(CancellationToken.None);
    }
}