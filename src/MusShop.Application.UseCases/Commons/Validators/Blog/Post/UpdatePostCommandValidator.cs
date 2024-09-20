using FluentValidation;
using MusShop.Application.UseCases.Commons.ValidatorRules.Blog;
using MusShop.Application.UseCases.Features.Blog.Posts.Commands.UpdatePostCommand;

namespace MusShop.Application.UseCases.Commons.Validators.Blog.Post;

public class UpdatePostCommandValidator: AbstractValidator<UpdatePostCommand>
{
    public UpdatePostCommandValidator()
    {
        RuleFor(command => command.PostActionDto.Title)
            .PostTitleRules();
        
        RuleFor(command => command.PostActionDto.Description)
            .PostDescriptionRules();
        
        RuleFor(command => command.PostActionDto.ImageUrl)
            .PostImageRules();
        
        RuleFor(command => command.PostActionDto.CategoryId)
            .PostCategoryRules();
    }
}