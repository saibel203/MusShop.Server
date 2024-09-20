using FluentValidation;
using MusShop.Domain.Model.ResultItems.ErrorsDocumentation;

namespace MusShop.Application.UseCases.Commons.ValidatorRules.Blog;

public static class PostPropertiesValidatorRules
{
    public static IRuleBuilderOptions<T, string> PostTitleRules<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithErrorCode(BlogErrors.PostTitleRequiredError.Code)
            .WithMessage(BlogErrors.PostTitleRequiredError.Description)
            .MaximumLength(200)
            .WithErrorCode(BlogErrors.PostTitleLengthError.Code)
            .WithMessage(BlogErrors.PostTitleLengthError.Description);
    }
    
    public static IRuleBuilderOptions<T, string> PostDescriptionRules<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithErrorCode(BlogErrors.PostDescriptionRequiredError.Code)
            .WithMessage(BlogErrors.PostDescriptionRequiredError.Description);
    }
    
    public static IRuleBuilderOptions<T, string> PostImageRules<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .MaximumLength(1000)
            .WithErrorCode(BlogErrors.PostImageLengthError.Code)
            .WithMessage(BlogErrors.PostImageLengthError.Description);
    }
    
    public static IRuleBuilderOptions<T, Guid> PostCategoryRules<T>(
        this IRuleBuilder<T, Guid> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithErrorCode(BlogErrors.PostCategoryRequiredError.Code)
            .WithMessage(BlogErrors.PostCategoryRequiredError.Description);
    }
}