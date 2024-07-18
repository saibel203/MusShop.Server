namespace MusShop.Domain.Model.ResultItems;

public sealed record DomainError(string Code, string Description)
{
    public static readonly DomainError None = new DomainError(string.Empty, string.Empty);
}