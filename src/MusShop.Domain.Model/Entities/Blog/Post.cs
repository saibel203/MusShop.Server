using MusShop.Domain.Model.Entities.Base;

namespace MusShop.Domain.Model.Entities.Blog;

public class Post : EntityMetadata<Guid>
{
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string ImageUrl { get; init; } = string.Empty;
    public string CreatedBy { get; init; } = string.Empty;
    
    public Guid CategoryId { get; init; }
    public Category Category { get; init; } = null!;
}