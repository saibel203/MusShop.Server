using MediatR;
using MusShop.Application.Dtos.Blog.Category;
using MusShop.Domain.Model.ResultItems;

namespace MusShop.Application.UseCases.Features.Blog.Categories.Commands.CreateCategoryCommand;

public record CreateCategoryCommand(CategoryActionDto CategoryActionDto) 
    : IRequest<DomainResult<CategoryDto>>;