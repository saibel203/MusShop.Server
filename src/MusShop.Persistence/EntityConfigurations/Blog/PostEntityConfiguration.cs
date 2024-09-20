using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusShop.Domain.Model.Entities.Blog;

namespace MusShop.Persistence.EntityConfigurations.Blog;

public class PostEntityConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("blog_posts");
        
        builder.HasKey(property => property.Id);

        builder.Property(property => property.Title)
            .HasColumnType("nvarchar(200)")
            .IsRequired();

        builder.Property(property => property.Description)
            .HasColumnType("nvarchar(max)")
            .IsRequired();

        builder.Property(property => property.ImageUrl)
            .HasColumnType("nvarchar(1000)")
            .IsRequired(false);

        builder.HasOne(property => property.Category)
            .WithMany(property => property.Posts)
            .HasForeignKey(property => property.CategoryId)
            .IsRequired();

        builder.Property(property => property.CreatedDate)
            .HasDefaultValueSql("getutcdate()");

        builder.Property(property => property.UpdatedDate)
            .HasDefaultValueSql("getutcdate()");
    }
}