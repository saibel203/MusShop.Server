namespace MusShop.Contracts.Responses;

public record BaseFilter(
    int? PageIndex,
    int? PageSize);