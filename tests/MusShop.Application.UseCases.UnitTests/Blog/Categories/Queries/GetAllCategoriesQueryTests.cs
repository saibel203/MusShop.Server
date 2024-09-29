using Mapster;
using MusShop.Application.Dtos.Blog.Category;
using MusShop.Application.UseCases.Features.Blog.Categories.Queries.GetAllCategoriesQuery;
using MusShop.Contracts.Filters;
using MusShop.Contracts.Responses;
using MusShop.Domain.Model.Entities.Blog;

namespace MusShop.Application.UseCases.UnitTests.Blog.Categories.Queries;

/*public class GetAllCategoriesQueryTests : BaseUnitTest
{
    [Theory, AutoNSubstituteData]
    public async Task GetAllCategories_NotEmptyResultList_ReturnCategoriesList(
        PaginatedList<Category> expectedResult)
    {
        // Arrange
        GetAllCategoriesHandler queryHandler =
            new GetAllCategoriesHandler(UnitOfWork, Mapper);
        int expectedResultCount = expectedResult.Items.Count();

        UnitOfWork.GetRepository<Category, CategoryFilter>().GetAll().Returns(expectedResult);
        Mapper.Adapt<PaginatedList<CategoryDto>>()
            .Returns(new PaginatedList<CategoryDto>(new List<CategoryDto>(), 0, 0, 0));

        // Act
        DomainResult<PaginatedList<CategoryDto>> getAllCategoriesResult =
            await queryHandler.Handle(new GetAllCategoriesQuery(), CancellationToken.None);

        // Assert
        getAllCategoriesResult.ShouldBeAssignableTo<DomainResult<IEnumerable<CategoryDto>>>();
        getAllCategoriesResult.IsSuccess.ShouldBeTrue();
        getAllCategoriesResult.IsFailure.ShouldBeFalse();
        getAllCategoriesResult.Error.Code.ShouldBeEmpty();
        getAllCategoriesResult.Error.Description.ShouldBeEmpty();
        getAllCategoriesResult.Errors.ShouldBeEmpty();
        getAllCategoriesResult.Value.Items.ShouldNotBeEmpty();
        getAllCategoriesResult.Value.Items.Count().ShouldBe(expectedResultCount);
    }

    [Fact]
    public async Task GetAllCategories_EmptyResultList_ReturnEmptyCategoriesList()
    {
        // Arrange
        GetAllCategoriesHandler queryHandler =
            new GetAllCategoriesHandler(UnitOfWork, Mapper);

        UnitOfWork.GetRepository<Category, CategoryFilter>().GetAll()
            .Returns(new PaginatedList<Category>(new List<Category>(),
                0, 0, 0));

        // Act
        DomainResult<PaginatedList<CategoryDto>> getAllCategoriesResult =
            await queryHandler.Handle(new GetAllCategoriesQuery(), CancellationToken.None);

        // Assert
        getAllCategoriesResult.ShouldBeAssignableTo<DomainResult<IEnumerable<CategoryDto>>>();
        getAllCategoriesResult.IsSuccess.ShouldBeTrue();
        getAllCategoriesResult.IsFailure.ShouldBeFalse();
        getAllCategoriesResult.Error.Code.ShouldBeEmpty();
        getAllCategoriesResult.Error.Description.ShouldBeEmpty();
        getAllCategoriesResult.Errors.ShouldBeEmpty();
        getAllCategoriesResult.Value.Items.ShouldBeEmpty();
    }
}*/