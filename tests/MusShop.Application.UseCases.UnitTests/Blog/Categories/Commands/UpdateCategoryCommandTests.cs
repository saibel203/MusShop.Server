using System.Net;
using MusShop.Application.Dtos.Blog.Category;
using MusShop.Application.UseCases.Commons.Behaviors;
using MusShop.Application.UseCases.Features.Blog.Categories.Commands.UpdateCategoryCommand;
using MusShop.Contracts.Filters;
using MusShop.Domain.Model.Entities.Blog;
using MusShop.Domain.Model.ResultItems.ErrorsDocumentation;
using NSubstitute.ReturnsExtensions;

namespace MusShop.Application.UseCases.UnitTests.Blog.Categories.Commands;

public class UpdateCategoryCommandTests : BaseUnitTest
{
    [Theory, AutoNSubstituteData]
    public async Task UpdateCategory_EmptyName_ReturnErrorResponse(
        Guid categoryId, CategoryActionDto categoryActionDto)
    {
        // Arrange
        UpdateCategoryHandler commandHandler = new UpdateCategoryHandler(UnitOfWork, Mapper);
        categoryActionDto.CategoryName = string.Empty;

        ValidationPipelineBehavior<UpdateCategoryCommand, DomainResult<CategoryActionDto>> pipeline =
            GetValidationPipeline<UpdateCategoryCommand, CategoryActionDto>(
                BlogErrors.CategoryNameRequiredError.Description, nameof(categoryActionDto.CategoryName));

        UpdateCategoryCommand invalidCommand = new UpdateCategoryCommand(categoryActionDto, categoryId)
        {
            CategoryActionDto = new CategoryActionDto { CategoryName = string.Empty }
        };

        Task<DomainResult<CategoryActionDto>> Next() =>
            commandHandler.Handle(invalidCommand, CancellationToken.None);

        // Act
        DomainResult<CategoryActionDto> result =
            await pipeline.Handle(invalidCommand, Next, CancellationToken.None);

        // Assert
        result.ShouldBeAssignableTo<DomainResult<CategoryActionDto>>();
        result.IsSuccess.ShouldBeFalse();
        result.IsFailure.ShouldBeTrue();
        result.Error.Description.ShouldBeEmpty();
        result.Error.Code.ShouldBeEmpty();
        result.Errors.ShouldNotBeEmpty();
        result.Errors[0].Description.ShouldBe(BlogErrors.CategoryNameRequiredError.Description);

        await UnitOfWork.DidNotReceive().CommitAsync(Arg.Any<CancellationToken>());
    }

    [Theory, AutoNSubstituteData]
    public async Task UpdateCategory_BadName_ReturnErrorResponse(
        Guid categoryId, CategoryActionDto categoryActionDto)
    {
        // Arrange
        UpdateCategoryHandler commandHandler = new UpdateCategoryHandler(UnitOfWork, Mapper);
        categoryActionDto.CategoryName = GenerateCustomLengthString(101);

        ValidationPipelineBehavior<UpdateCategoryCommand, DomainResult<CategoryActionDto>> pipeline =
            GetValidationPipeline<UpdateCategoryCommand, CategoryActionDto>(
                BlogErrors.CategoryNameLengthError.Description, nameof(categoryActionDto.CategoryName));

        UpdateCategoryCommand invalidCommand = new UpdateCategoryCommand(categoryActionDto, categoryId)
        {
            CategoryActionDto = new CategoryActionDto { CategoryName = GenerateCustomLengthString(101) }
        };

        Task<DomainResult<CategoryActionDto>> Next() =>
            commandHandler.Handle(invalidCommand, CancellationToken.None);

        // Act
        DomainResult<CategoryActionDto> result =
            await pipeline.Handle(invalidCommand, Next, CancellationToken.None);

        // Assert
        result.ShouldBeAssignableTo<DomainResult<CategoryActionDto>>();
        result.IsSuccess.ShouldBeFalse();
        result.IsFailure.ShouldBeTrue();
        result.Error.Description.ShouldBeEmpty();
        result.Error.Code.ShouldBeEmpty();
        result.Errors.ShouldNotBeEmpty();
        result.Errors[0].Description.ShouldBe(BlogErrors.CategoryNameLengthError.Description);

        await UnitOfWork.DidNotReceive().CommitAsync(Arg.Any<CancellationToken>());
    }

    [Theory, AutoNSubstituteData]
    public async Task UpdateCategory_NonExistingCategoryWithId_ReturnErrorResponse(
        Guid categoryId, CategoryActionDto categoryActionDto)
    {
        // Arrange
        UpdateCategoryHandler commandHandler = new UpdateCategoryHandler(UnitOfWork, Mapper);

        UnitOfWork.GetRepository<Category, CategoryFilter>().GetById(categoryId).ReturnsNull();

        // Act
        DomainResult<CategoryActionDto> updateCategoryResult =
            await commandHandler.Handle(new UpdateCategoryCommand(categoryActionDto, categoryId),
                CancellationToken.None);

        // Assert
        updateCategoryResult.ShouldBeAssignableTo<DomainResult<CategoryActionDto>>();
        updateCategoryResult.IsSuccess.ShouldBeFalse();
        updateCategoryResult.IsFailure.ShouldBeTrue();
        updateCategoryResult.Error.Description.ShouldBe(BlogErrors.CategoryNotFound.Description);
        updateCategoryResult.Error.Code.ShouldBe(BlogErrors.CategoryNotFound.Code);
        updateCategoryResult.Error.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        updateCategoryResult.Errors.ShouldBeEmpty();
    }

    [Theory, AutoNSubstituteData]
    public async Task Update_ValidData_ReturnNewCategoryResponse(
        CategoryActionDto categoryActionDto, Guid guid, Category category)
    {
        // Arrange
        UpdateCategoryHandler commandHandler = new UpdateCategoryHandler(UnitOfWork, Mapper);

        UnitOfWork.GetRepository<Category, CategoryFilter>().GetById(guid).Returns(category);

        // Act
        DomainResult<CategoryActionDto> updatePostResult =
            await commandHandler.Handle(new UpdateCategoryCommand(categoryActionDto, guid), CancellationToken.None);

        // Assert
        updatePostResult.ShouldBeAssignableTo<DomainResult<CategoryActionDto>>();
        updatePostResult.IsSuccess.ShouldBeTrue();
        updatePostResult.IsFailure.ShouldBeFalse();
        updatePostResult.Error.Description.ShouldBeEmpty();
        updatePostResult.Error.Code.ShouldBeEmpty();
        updatePostResult.Errors.ShouldBeEmpty();
        updatePostResult.Value.ShouldNotBeNull();

        UnitOfWork.GetRepository<Category, CategoryFilter>().Received(BaseReceivedCount).Update(Arg.Any<Category>());
        await UnitOfWork.Received(BaseReceivedCount).CommitAsync(CancellationToken.None);
    }
}