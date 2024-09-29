using Mapster;
using MusShop.Contracts.Responses;

namespace MusShop.Application.MapsterConfigurations.Helpers;

public static class MapsterConfigHelper
{
    public static void ConfigPaginatedEntities<TFirst, TSecond>()
    {
        TypeAdapterConfig<PaginatedList<TFirst>, PaginatedList<TSecond>>.NewConfig()
            .Map(dest => dest.Items, src => src.Items.Adapt<IEnumerable<TSecond>>())
            .Map(dest => dest.PageIndex, src => src.PageIndex)
            .Map(dest => dest.PageSize, src => src.PageSize)
            .Map(dest => dest.TotalPages, src => src.TotalPages)
            .ConstructUsing(src => new PaginatedList<TSecond>(
                src.Items.Adapt<List<TSecond>>(),
                src.PageIndex,
                src.TotalRecords,
                src.PageSize
            ));
    }
}