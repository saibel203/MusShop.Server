namespace MusShop.Application.Dtos.Blog.Post;

public class PostActionDto
{
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string ImageUrl { get; init; } = string.Empty;
    public Guid CategoryId { get; init; }
}