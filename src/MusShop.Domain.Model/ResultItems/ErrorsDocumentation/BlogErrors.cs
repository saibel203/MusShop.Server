using System.Net;

namespace MusShop.Domain.Model.ResultItems.ErrorsDocumentation;

public static class BlogErrors
{
    public static readonly DomainError CategoryNotFound = 
        new(
            "BlogErrors.CategoryNotFound", 
            "Category with the specified Id was not found",
            HttpStatusCode.NotFound);
    
    public static readonly DomainError CategoryNameRequiredError = 
        new(
            "BlogErrors.CategoryNameRequiredError", 
            "The 'Category Name' field is required");
    
    public static readonly DomainError CategoryNameLengthError = 
        new(
            "BlogErrors.CategoryNameLengthError", 
            "The 'Category Name' field length should be no more than 100 characters");
    
    public static readonly DomainError PostNotFound = 
        new(
            "BlogErrors.PostNotFound", 
            "Post with the specified Id was not found",
            HttpStatusCode.NotFound);
    
    public static readonly DomainError PostTitleRequiredError = 
        new(
            "BlogErrors.PostTitleRequiredError", 
            "The 'Title' field is required");
    
    public static readonly DomainError PostTitleLengthError = 
        new(
            "BlogErrors.PostTitleLengthError", 
            "The 'Title' field length should be no more than 200 characters");
    
    public static readonly DomainError PostDescriptionRequiredError = 
        new(
            "BlogErrors.PostDescriptionRequiredError", 
            "The 'Description' field is required");
    
    public static readonly DomainError PostImageLengthError = 
        new(
            "BlogErrors.PostImageLengthError", 
            "The 'Image' field length should be no more than 1000 characters");
    
    public static readonly DomainError PostCategoryRequiredError = 
        new(
            "BlogErrors.PostCategoryRequiredError", 
            "The 'Category' field is required");
}