using System.Net;
using MusShop.Application.Dtos.Blog.Category;
using MusShop.Application.UseCases.Features.Blog.Categories.Commands.DeleteCategoryCommand;
using MusShop.Contracts.Filters;
using MusShop.Domain.Model.Entities.Blog;
using MusShop.Domain.Model.ResultItems.ErrorsDocumentation;
using NSubstitute.ReturnsExtensions;

namespace MusShop.Application.UseCases.UnitTests.Blog.Categories.Commands;

public class DeleteCategoryCommandTests : BaseUnitTest
{
    [Theory, AutoNSubstituteData]
    public async Task DeleteCategory_NonExistingCategoryWithId_ReturnErrorResponse(
        Guid categoryId)
    {
        // Arrange
        DeleteCategoryHandler commandHandler = new DeleteCategoryHandler(UnitOfWork, Mapper);

        UnitOfWork.GetRepository<Category, CategoryFilter>().GetById(categoryId).ReturnsNull();

        // Act
        DomainResult<CategoryDto> deleteCategoryResult =
            await commandHandler.Handle(new DeleteCategoryCommand(categoryId), CancellationToken.None);

        // Assert
        deleteCategoryResult.ShouldBeAssignableTo<DomainResult<CategoryDto>>();
        deleteCategoryResult.IsSuccess.ShouldBeFalse();
        deleteCategoryResult.IsFailure.ShouldBeTrue();
        deleteCategoryResult.Error.Description.ShouldBe(BlogErrors.CategoryNotFound.Description);
        deleteCategoryResult.Error.Code.ShouldBe(BlogErrors.CategoryNotFound.Code);
        deleteCategoryResult.Error.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        deleteCategoryResult.Errors.ShouldBeEmpty();
    }
    
    [Theory, AutoNSubstituteData]
    public async Task DeleteCategory_ExistingCategoryWithId_ReturnEmptyResponse(
        Guid categoryId, Category category)
    {
        // Arrange
        DeleteCategoryHandler commandHandler = new DeleteCategoryHandler(UnitOfWork, Mapper);

        UnitOfWork.GetRepository<Category, CategoryFilter>().GetById(categoryId).Returns(category);

        // Act
        DomainResult<CategoryDto> deleteCategoryResult =
            await commandHandler.Handle(new DeleteCategoryCommand(categoryId), CancellationToken.None);
        
        // Assert
        deleteCategoryResult.ShouldBeAssignableTo<DomainResult<CategoryDto>>();
        deleteCategoryResult.IsSuccess.ShouldBeTrue();
        deleteCategoryResult.IsFailure.ShouldBeFalse();
        deleteCategoryResult.Error.Description.ShouldBeEmpty();
        deleteCategoryResult.Error.Code.ShouldBeEmpty();
        deleteCategoryResult.Errors.ShouldBeEmpty();
        
        UnitOfWork.GetRepository<Category, CategoryFilter>().Received(BaseReceivedCount).Delete(category);
        await UnitOfWork.Received(BaseReceivedCount).CommitAsync(CancellationToken.None);
    }
}