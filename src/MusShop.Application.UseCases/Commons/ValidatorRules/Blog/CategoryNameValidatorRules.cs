using FluentValidation;
using MusShop.Domain.Model.ResultItems.ErrorsDocumentation;

namespace MusShop.Application.UseCases.Commons.ValidatorRules.Blog;

public static class CategoryNameValidatorRules
{
    public static IRuleBuilderOptions<T, string> CategoryNameRules<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithErrorCode(BlogErrors.CategoryNameRequiredError.Code)
            .WithMessage(BlogErrors.CategoryNameRequiredError.Description)
            .MaximumLength(100)
            .WithErrorCode(BlogErrors.CategoryNameLengthError.Code)
            .WithMessage(BlogErrors.CategoryNameLengthError.Description);
    }
}