using Mapster;
using MusShop.Application.Dtos.Blog.Category;
using MusShop.Application.Dtos.Blog.Post;
using MusShop.Application.MapsterConfigurations.Helpers;
using MusShop.Domain.Model.Entities.Blog;

namespace MusShop.Application.MapsterConfigurations;

public static class BlogMapsterConfig
{
    public static void Configure()
    {
        TypeAdapterConfig<Category, CategoryDto>.NewConfig()
            .Map(dest => dest.CategoryName, src => src.CategoryName);

        TypeAdapterConfig<CategoryActionDto, Category>.NewConfig()
            .Map(dest => dest.CategoryName, src => src.CategoryName)
            .TwoWays();

        TypeAdapterConfig<Post, PostDto>.NewConfig()
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.ImageUrl, src => src.ImageUrl)
            .Map(dest => dest.CreatedBy, src => src.CreatedBy)
            .Map(dest => dest.CategoryId, src => src.CategoryId)
            .Map(dest => dest.CreatedDate, src => src.CreatedDate)
            .Map(dest => dest.UpdatedDate, src => src.UpdatedDate);

        TypeAdapterConfig<Post, PostDto>.NewConfig()
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.ImageUrl, src => src.ImageUrl)
            .Map(dest => dest.CreatedBy, src => src.CreatedBy)
            .Map(dest => dest.CategoryId, src => src.CategoryId)
            .Map(dest => dest.CreatedDate, src => src.CreatedDate)
            .Map(dest => dest.UpdatedDate, src => src.UpdatedDate)
            .TwoWays();
        
        MapsterConfigHelper.ConfigPaginatedEntities<Post, PostDto>();
        MapsterConfigHelper.ConfigPaginatedEntities<Category, CategoryDto>();
    }
}