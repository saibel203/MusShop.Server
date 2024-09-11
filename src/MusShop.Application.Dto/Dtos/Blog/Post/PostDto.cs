namespace MusShop.Application.Dtos.Blog.Post;

public class PostDto
{
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string ImageUrl { get; init; } = string.Empty;
    public string CreatedBy { get; init; } = string.Empty;

    public Guid CategoryId { get; init; }
    
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}