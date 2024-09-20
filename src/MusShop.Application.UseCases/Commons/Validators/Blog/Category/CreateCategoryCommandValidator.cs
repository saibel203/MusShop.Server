using FluentValidation;
using MusShop.Application.UseCases.Commons.ValidatorRules.Blog;
using MusShop.Application.UseCases.Features.Blog.Categories.Commands.CreateCategoryCommand;

namespace MusShop.Application.UseCases.Commons.Validators.Blog.Category;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(command => command.CategoryActionDto.CategoryName)
            .CategoryNameRules();
    }
}