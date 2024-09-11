namespace MusShop.Domain.Model.ResultItems.ErrorsDocumentation;

public static class BlogErrors
{
    public static readonly DomainError CategoryNotFound = 
        new DomainError(
            "BlogErrors.CategoryNotFound", 
            "Category with the specified Id was not found");
    
    public static readonly DomainError CategoryNameError = 
        new DomainError(
            "BlogErrors.CategoryNameError", 
            "Category name is invalid");
    
    public static readonly DomainError PostNotFound = 
        new DomainError(
            "BlogErrors.PostNotFound", 
            "Post with the specified Id was not found");
}