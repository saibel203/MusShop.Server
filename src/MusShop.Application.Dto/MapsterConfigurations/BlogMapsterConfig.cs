using Mapster;
using MusShop.Application.Dtos.Blog;
using MusShop.Domain.Model.Entities.Blog;

namespace MusShop.Application.MapsterConfigurations;

public static class BlogMapsterConfig
{
    public static void Configure()
    {
        TypeAdapterConfig<Category, CategoryDto>.NewConfig()
            .Map(dest => dest.CategoryName, src => src.CategoryName);
    }
}