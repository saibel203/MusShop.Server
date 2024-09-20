using FluentValidation;
using MusShop.Application.UseCases.Commons.ValidatorRules.Blog;
using MusShop.Application.UseCases.Features.Blog.Categories.Commands.UpdateCategoryCommand;

namespace MusShop.Application.UseCases.Commons.Validators.Blog.Category;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(command => command.CategoryActionDto.CategoryName)
            .CategoryNameRules();
    }
}