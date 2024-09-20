using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusShop.Domain.Model.Entities.Blog;

namespace MusShop.Persistence.EntityConfigurations.Blog;

public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("blog_categories");
        
        builder.HasKey(property => property.Id);

        builder.Property(property => property.CategoryName)
            .HasColumnType("nvarchar(100)")
            .IsRequired();

        builder.Property(property => property.CreatedDate)
            .HasDefaultValueSql("getutcdate()");

        builder.Property(property => property.UpdatedDate)
            .HasDefaultValueSql("getutcdate()");
    }
}