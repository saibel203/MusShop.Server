using MusShop.Contracts.Responses;

namespace MusShop.Contracts.Filters;

public record PostFilter(
    int? PageIndex,
    int? PageSize) : BaseFilter(PageIndex, PageSize);