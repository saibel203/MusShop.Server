namespace MusShop.Domain.Model.Entities.Base;

public class BaseEntity
{
    public Guid Id { get; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}