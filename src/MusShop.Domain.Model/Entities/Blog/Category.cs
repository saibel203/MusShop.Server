using MusShop.Domain.Model.Entities.Base;

namespace MusShop.Domain.Model.Entities.Blog;

public class Category : BaseEntity<Guid>
{
    public string CategoryName { get; init; } = string.Empty;
    public ICollection<Post> Posts { get; } = new List<Post>();
}