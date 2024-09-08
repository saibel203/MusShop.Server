namespace MusShop.Domain.Model.Entities.Base;

public class EntityMetadata<TId> : BaseEntity<TId>
{
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}