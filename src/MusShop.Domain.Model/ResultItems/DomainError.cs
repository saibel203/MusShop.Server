using System.Net;

namespace MusShop.Domain.Model.ResultItems;

public record DomainError(
    string Code,
    string Description,
    HttpStatusCode StatusCode = HttpStatusCode.BadRequest)
{
    public static readonly DomainError None =
        new(string.Empty, string.Empty);

    public static readonly DomainError FewErrors =
        new(string.Empty, string.Empty);
}