using Mapster;
using MapsterMapper;
using MusShop.Application.MapsterConfigurations;

namespace MusShop.UnitTests.Shared.Mocks;

public static class MapsterMock
{
    public static Mapper GetMapper()
    {
        TypeAdapterConfig config = TypeAdapterConfig.GlobalSettings;
        config.Scan(typeof(BaseMapsterConfig).Assembly);
        return new Mapper(config);
    }
}