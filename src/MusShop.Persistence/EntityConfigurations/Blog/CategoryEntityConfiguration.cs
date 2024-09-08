using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusShop.Domain.Model.Entities.Blog;

namespace MusShop.Persistence.EntityConfigurations.Blog;

public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(property => property.Id);

        builder.Property(property => property.CategoryName)
            .HasColumnType("nvarchar(100)")
            .IsRequired();
    }
}