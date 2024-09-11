using MediatR;
using MusShop.Application.Dtos.Blog.Category;
using MusShop.Domain.Model.ResultItems;

namespace MusShop.Application.UseCases.Features.Blog.Categories.Commands.UpdateCategoryCommand;

public record UpdateCategoryCommand(CategoryActionDto CategoryActionDto, Guid CategoryId) 
    : IRequest<DomainResult<CategoryActionDto>>;