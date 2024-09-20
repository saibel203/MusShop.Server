namespace MusShop.Application.Dtos.Blog.Post;

public class PostActionDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
}