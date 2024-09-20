using MusShop.Application.Dtos.Blog.Category;
using MusShop.Application.UseCases.Features.Blog.Categories.Queries.GetAllCategoriesQuery;
using MusShop.Domain.Model.Entities.Blog;

namespace MusShop.Application.UseCases.UnitTests.Blog.Categories.Queries;

public class GetAllCategoriesQueryTests : BaseUnitTest
{
    [Theory, AutoNSubstituteData]
    public async Task GetAllCategories_NotEmptyResultList_ReturnCategoriesList(
        IReadOnlyList<Category> expectedResult)
    {
        // Arrange
        GetAllCategoriesHandler queryHandler =
            new GetAllCategoriesHandler(UnitOfWork, Mapper);
        int expectedResultCount = expectedResult.Count;

        UnitOfWork.GetRepository<Category>().GetAll().Returns(expectedResult.AsEnumerable());

        // Act
        DomainResult<IEnumerable<CategoryDto>> getAllCategoriesResult =
            await queryHandler.Handle(new GetAllCategoriesQuery(), CancellationToken.None);

        // Assert
        getAllCategoriesResult.ShouldBeAssignableTo<DomainResult<IEnumerable<CategoryDto>>>();
        getAllCategoriesResult.IsSuccess.ShouldBeTrue();
        getAllCategoriesResult.IsFailure.ShouldBeFalse();
        getAllCategoriesResult.Error.Code.ShouldBeEmpty();
        getAllCategoriesResult.Error.Description.ShouldBeEmpty();
        getAllCategoriesResult.Errors.ShouldBeEmpty();
        getAllCategoriesResult.Value.ShouldNotBeEmpty();
        getAllCategoriesResult.Value.Count().ShouldBe(expectedResultCount);
    }

    [Fact]
    public async Task GetAllCategories_EmptyResultList_ReturnEmptyCategoriesList()
    {
        // Arrange
        GetAllCategoriesHandler queryHandler =
            new GetAllCategoriesHandler(UnitOfWork, Mapper);

        UnitOfWork.GetRepository<Category>().GetAll().Returns(Enumerable.Empty<Category>());

        // Act
        DomainResult<IEnumerable<CategoryDto>> getAllCategoriesResult =
            await queryHandler.Handle(new GetAllCategoriesQuery(), CancellationToken.None);

        // Assert
        getAllCategoriesResult.ShouldBeAssignableTo<DomainResult<IEnumerable<CategoryDto>>>();
        getAllCategoriesResult.IsSuccess.ShouldBeTrue();
        getAllCategoriesResult.IsFailure.ShouldBeFalse();
        getAllCategoriesResult.Error.Code.ShouldBeEmpty();
        getAllCategoriesResult.Error.Description.ShouldBeEmpty();
        getAllCategoriesResult.Errors.ShouldBeEmpty();
        getAllCategoriesResult.Value.ShouldBeEmpty();
    }
}