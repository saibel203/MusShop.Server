using System.Net;
using MusShop.Application.Dtos.Blog.Category;
using MusShop.Application.UseCases.Features.Blog.Categories.Queries.GetCategoryByIdQuery;
using MusShop.Contracts.Filters;
using MusShop.Domain.Model.Entities.Blog;
using MusShop.Domain.Model.ResultItems.ErrorsDocumentation;
using NSubstitute.ReturnsExtensions;

namespace MusShop.Application.UseCases.UnitTests.Blog.Categories.Queries;

public class GetCategoryByIdQueryTests : BaseUnitTest
{
    [Theory, AutoNSubstituteData]
    public async Task GetCategoryById_NonExistingCategoryWithId_ReturnErrorResponse(
        Guid categoryId)
    {
        // Arrange
        GetCategoryByIdHandler queryHandler = new GetCategoryByIdHandler(UnitOfWork, Mapper);

        UnitOfWork.GetRepository<Category, CategoryFilter>().GetById(categoryId).ReturnsNull();

        // Act
        DomainResult<CategoryDto> getCategoryByIdResult =
            await queryHandler.Handle(new GetCategoryByIdQuery(categoryId), CancellationToken.None);

        // Assert
        getCategoryByIdResult.ShouldBeAssignableTo<DomainResult<CategoryDto>>();
        getCategoryByIdResult.IsSuccess.ShouldBeFalse();
        getCategoryByIdResult.IsFailure.ShouldBeTrue();
        getCategoryByIdResult.Error.Description.ShouldBe(BlogErrors.CategoryNotFound.Description);
        getCategoryByIdResult.Error.Code.ShouldBe(BlogErrors.CategoryNotFound.Code);
        getCategoryByIdResult.Error.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        getCategoryByIdResult.Errors.ShouldBeEmpty();
    }
    
    [Theory, AutoNSubstituteData]
    public async Task GetCategoryById_ExistingCategoryWithId_ReturnCategoryResponse(
        Guid categoryId, Category category)
    {
        // Arrange
        GetCategoryByIdHandler queryHandler = new GetCategoryByIdHandler(UnitOfWork, Mapper);

        UnitOfWork.GetRepository<Category, CategoryFilter>().GetById(categoryId).Returns(category);

        // Act
        DomainResult<CategoryDto> getCategoryByIdResult =
            await queryHandler.Handle(new GetCategoryByIdQuery(categoryId), CancellationToken.None);

        // Assert
        getCategoryByIdResult.ShouldBeAssignableTo<DomainResult<CategoryDto>>();
        getCategoryByIdResult.IsSuccess.ShouldBeTrue();
        getCategoryByIdResult.IsFailure.ShouldBeFalse();
        getCategoryByIdResult.Error.Description.ShouldBeEmpty();
        getCategoryByIdResult.Error.Code.ShouldBeEmpty();
        getCategoryByIdResult.Errors.ShouldBeEmpty();
        getCategoryByIdResult.Value.ShouldNotBeNull();
        getCategoryByIdResult.Value.CategoryName.ShouldNotBeEmpty();
    }
}