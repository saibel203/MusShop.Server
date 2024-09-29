using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusShop.Application.Dtos.Blog.Category;
using MusShop.Application.Dtos.Blog.Post;
using MusShop.Application.UseCases.Features.Blog.Categories.Commands.CreateCategoryCommand;
using MusShop.Application.UseCases.Features.Blog.Categories.Commands.DeleteCategoryCommand;
using MusShop.Application.UseCases.Features.Blog.Categories.Commands.UpdateCategoryCommand;
using MusShop.Application.UseCases.Features.Blog.Categories.Queries.GetAllCategoriesQuery;
using MusShop.Application.UseCases.Features.Blog.Categories.Queries.GetCategoryByIdQuery;
using MusShop.Application.UseCases.Features.Blog.Posts.Commands.CreatePostCommand;
using MusShop.Application.UseCases.Features.Blog.Posts.Commands.DeletePostCommand;
using MusShop.Application.UseCases.Features.Blog.Posts.Commands.UpdatePostCommand;
using MusShop.Application.UseCases.Features.Blog.Posts.Queries.GetAllPostsQuery;
using MusShop.Application.UseCases.Features.Blog.Posts.Queries.GetPostByIdQuery;
using MusShop.Contracts.Filters;
using MusShop.Contracts.Responses;
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

    /// <summary>
    /// Receives all blog categories
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Successfully obtaining a blog categories</response>
    [HttpGet("category/all")] // /api/blog/category/all
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAllCategories()
    {
        DomainResult<IEnumerable<CategoryDto>> getCategoriesResult =
            await _mediator.Send(new GetAllCategoriesQuery());

        return Ok(getCategoriesResult.Value);
    }

    /// <summary>
    /// Receives blog category by ID
    /// </summary>
    /// <param name="id">Category id</param>
    /// <returns></returns>
    /// <response code="200">Successfully obtaining a blog category by ID</response>
    /// <response code="404">Category with the specified ID not found</response>
    [HttpGet("category/{id:guid}")] // /api/blog/category/{id}
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetCategoryById(Guid id)
    {
        DomainResult<CategoryDto> getCategoryResult =
            await _mediator.Send(new GetCategoryByIdQuery(id));

        return getCategoryResult.Match<CategoryDto, IActionResult>(
            onSuccess: () => Ok(getCategoryResult.Value),
            onError: error => StatusCode(error.StatusCode, getCategoryResult.Error),
            onErrors: BadRequest);
    }

    /// <summary>
    /// Creates a new blog category and returns it
    /// </summary>
    /// <remarks>
    ///     Request example:
    ///
    ///         POST /api/blog/category/create
    ///         {
    ///             "CategoryName": "Some category name"
    ///         }
    /// </remarks>
    /// <param name="categoryActionDto">Category</param>
    /// <returns></returns>
    /// <response code="200">Category successfully created</response>
    /// <response code="400">Error trying to create a category</response>
    [HttpPost("category/create")] // /api/blog/category/create
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateCategory(CategoryActionDto categoryActionDto)
    {
        DomainResult<CategoryDto> createCategoryResult =
            await _mediator.Send(new CreateCategoryCommand(categoryActionDto));

        return createCategoryResult.Match<CategoryDto, IActionResult>(
            onSuccess: () => Ok(createCategoryResult.Value),
            onError: error => StatusCode(error.StatusCode, error),
            onErrors: BadRequest);
    }

    /// <summary>
    /// Updates the blog category by its ID
    /// </summary>
    /// <remarks>
    ///     Request example:
    ///
    ///         PUT /api/blog/category/edit/{id}
    ///         {
    ///             "CategoryName": "Some category name"
    ///         }
    /// </remarks>
    /// <param name="id">Category ID</param>
    /// <param name="editCategoryDto">Category</param>
    /// <returns></returns>
    /// <response code="200">Category successfully updated</response>
    /// <response code="400">Error trying to update a category</response>
    /// <response code="404">Category with the specified ID not found</response>
    [HttpPut("category/edit/{id:guid}")] // /api/blog/category/edit/{id}
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> EditCategory(Guid id, [FromBody] CategoryActionDto editCategoryDto)
    {
        DomainResult<CategoryActionDto> editCategoryResult =
            await _mediator.Send(new UpdateCategoryCommand(editCategoryDto, id));

        return editCategoryResult.Match<CategoryActionDto, IActionResult>(
            onSuccess: () => Ok(editCategoryResult.Value),
            onError: error => StatusCode(error.StatusCode, error),
            onErrors: BadRequest);
    }

    /// <summary>
    /// Deletes a blog category by its ID
    /// </summary>
    /// <param name="id">Category ID</param>
    /// <returns></returns>
    /// <response code="204">Successfully delete a blog category by ID</response>
    /// <response code="404">Category with the specified ID not found</response>
    [HttpDelete("category/delete/{id:guid}")] // /api/blog/category/delete/{id}
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        DomainResult<CategoryDto> deleteCategoryResult =
            await _mediator.Send(new DeleteCategoryCommand(id));

        return deleteCategoryResult.Match<CategoryDto, IActionResult>(
            onSuccess: NoContent,
            onError: error => StatusCode(deleteCategoryResult.Error.StatusCode, error),
            onErrors: BadRequest);
    }

    /// <summary>
    /// Receives all blog posts
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Successfully obtaining a blog posts</response>
    [HttpGet("posts")] // /api/blog/posts
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAllPosts([FromQuery] PostFilter? filter)
    {
        DomainResult<PaginatedList<PostDto>> getPostsResult =
            await _mediator.Send(new GetAllPostsQuery(filter));
        
        return Ok(getPostsResult.Value);
    }

    /// <summary>
    /// Receives blog post by ID
    /// </summary>
    /// <param name="id">Post ID</param>
    /// <returns></returns>
    /// <response code="200">Successfully obtaining a blog post by ID</response>
    /// <response code="404">Post the specified ID not found</response>
    [HttpGet("post/{id:guid}")] // /api/blog/post/{id}
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetPostById(Guid id)
    {
        DomainResult<PostDto> getPostResult =
            await _mediator.Send(new GetPostByIdQuery(id));

        return getPostResult.Match<PostDto, IActionResult>(
            onSuccess: () => Ok(getPostResult.Value),
            onError: error => StatusCode(error.StatusCode, error),
            onErrors: BadRequest);
    }

    /// <summary>
    /// Creates a new blog post and returns it
    /// </summary>
    /// <remarks>
    ///     Request example:
    ///
    ///         POST /api/blog/post/create
    ///         {
    ///             "Title": "Some title"
    ///             "Description": "Some description"
    ///             "ImageUrl": "Some image url"
    ///             "CategoryId": "e1d47a95-bc41-4ec7-9922-5b311a46339b"
    ///         }
    /// </remarks>
    /// <param name="createPostDto">Post</param>
    /// <returns></returns>
    /// <response code="200">Post successfully created</response>
    /// <response code="400">Error trying to create a post</response>
    [HttpPost("post/create")] // /api/blog/post/create
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreatePost(PostActionDto createPostDto)
    {
        DomainResult<PostDto> createPostResult =
            await _mediator.Send(new CreatePostCommand(createPostDto));

        return createPostResult.Match<PostDto, IActionResult>(
            onSuccess: () => Ok(createPostResult.Value),
            onError: error => StatusCode(error.StatusCode, error),
            onErrors: BadRequest);
    }

    /// <summary>
    /// Updates the blog post by its ID
    /// </summary>
    /// <remarks>
    ///     Request example:
    ///
    ///         PUT /api/blog/post/update/{id}
    ///         {
    ///             "Title": "Some title"
    ///             "Description": "Some description"
    ///             "ImageUrl": "Some image url"
    ///             "CategoryId": "e1d47a95-bc41-4ec7-9922-5b311a46339b"
    ///         }
    /// </remarks>
    /// <param name="updatePostDto">Post</param>
    /// <param name="id">Post ID</param>
    /// <returns></returns>
    /// <response code="200">Post successfully updated</response>
    /// <response code="400">Error trying to update a post</response>
    /// <response code="404">Post with the specified ID not found</response>
    [HttpPut("post/edit/{id:guid}")] // /api/post/edit/{id}
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> EditPost(Guid id, PostActionDto updatePostDto)
    {
        DomainResult<PostDto> updatePostResult =
            await _mediator.Send(new UpdatePostCommand(updatePostDto, id));

        return updatePostResult.Match<PostDto, IActionResult>(
            onSuccess: () => Ok(updatePostResult.Value),
            onError: error => StatusCode(error.StatusCode, error),
            onErrors: BadRequest);
    }

    /// <summary>
    /// Deletes a blog post by its ID
    /// </summary>
    /// <param name="id">Post ID</param>
    /// <returns></returns>
    /// <response code="204">Successfully delete a blog post by ID</response>
    /// <response code="404">Post with the specified ID not found</response>
    [HttpDelete("post/delete/{id:guid}")] // /api/post/delete/{id}
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> DeletePost(Guid id)
    {
        DomainResult<PostDto> deletePostResult =
            await _mediator.Send(new DeletePostCommand(id));

        return deletePostResult.Match<PostDto, IActionResult>(
            onSuccess: NoContent,
            onError: error => StatusCode(error.StatusCode, error),
            onErrors: BadRequest);
    }
}