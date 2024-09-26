namespace MusShop.Application.Dtos.Blog.Post;

/// <summary>
/// Blog post
/// </summary>
public class PostActionDto
{
    /// <summary>
    /// Title
    /// </summary>
    public required string Title { get; set; }
    
    /// <summary>
    /// Description
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// Image Url
    /// </summary>
    public string ImageUrl { get; set; } = string.Empty;
    
    /// <summary>
    /// Category ID
    /// </summary>
    public Guid CategoryId { get; set; }
}