using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusShop.Application.UseCases.Features.Blog.Categories.Queries.GetAllCategoriesQuery;
using MusShop.Presentation.Controllers.Base;

namespace MusShop.Presentation.Controllers;

public class BlogController : BaseController
{
    private readonly ISender _mediator;

    public BlogController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("categories")]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _mediator.Send(new GetAllCategoriesQuery());
        return Ok(categories);
    }
}