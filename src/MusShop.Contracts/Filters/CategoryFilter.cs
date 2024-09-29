using MusShop.Contracts.Responses;

namespace MusShop.Contracts.Filters;

public record CategoryFilter(
    int? PageIndex,
    int? PageSize) : BaseFilter(PageIndex, PageSize);