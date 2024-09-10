using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusShop.Application.Dtos.Blog.Category;
using MusShop.Application.UseCases.Features.Blog.Categories.Commands.CreateCategoryCommand;
using MusShop.Application.UseCases.Features.Blog.Categories.Commands.DeleteCategoryCommand;
using MusShop.Application.UseCases.Features.Blog.Categories.Commands.UpdateCategoryCommand;
using MusShop.Application.UseCases.Features.Blog.Categories.Queries.GetAllCategoriesQuery;
using MusShop.Application.UseCases.Features.Blog.Categories.Queries.GetCategoryByIdQuery;
using MusShop.Domain.Model.ResultItems;
using MusShop.Domain.Model.ResultItems.Extensions;
using MusShop.Presentation.Controllers.Base;

namespace MusShop.Presentation.Controllers;

public class BlogController : BaseController
{
    private readonly ISender _mediator;

    public BlogController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("categories")] // /api/blog/categories
    public async Task<IActionResult> GetAllCategories()
    {
        DomainResult<IEnumerable<CategoryDto>> getCategoriesResult =
            await _mediator.Send(new GetAllCategoriesQuery());
        return Ok(getCategoriesResult.Value);
    }

    [HttpGet("category/{id:guid}")] // /api/blog/category/{id}
    public async Task<IActionResult> GetCategoryById(Guid id)
    {
        DomainResult<CategoryDto> getCategoryResult =
            await _mediator.Send(new GetCategoryByIdQuery(id));

        return getCategoryResult.Match<CategoryDto, IActionResult>(
            onSuccess: () => Ok(getCategoryResult.Value),
            onError: BadRequest);
    }

    [HttpPost("category/create")] // /api/blog/category/create
    public async Task<IActionResult> CreateCategory(CategoryActionDto categoryActionDto)
    {
        DomainResult<CategoryDto> createCategoryResult =
            await _mediator.Send(new CreateCategoryCommand(categoryActionDto));

        return createCategoryResult.Match<CategoryDto, IActionResult>(
            onSuccess: () => Ok(createCategoryResult.Value),
            onError: BadRequest);
    }

    [HttpPut("category/edit/{id:guid}")] // /api/blog/category/edit/{id}
    public async Task<IActionResult> EditCategory(Guid id, [FromBody] CategoryActionDto editCategoryDto)
    {
        DomainResult<CategoryActionDto> editCategoryResult =
            await _mediator.Send(new UpdateCategoryCommand(editCategoryDto, id));

        return editCategoryResult.Match<CategoryActionDto, IActionResult>(
            onSuccess: () => Ok(editCategoryResult.Value),
            onError: BadRequest);
    }

    [HttpDelete("category/delete/{id:guid}")] // /api/blog/category/delete/{id}
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        DomainResult<CategoryDto> deleteCategoryResult =
            await _mediator.Send(new DeleteCategoryCommand(id));

        return deleteCategoryResult.Match<CategoryDto, IActionResult>(
            onSuccess: () => Ok(deleteCategoryResult.Value),
            onError: BadRequest);
    }
}