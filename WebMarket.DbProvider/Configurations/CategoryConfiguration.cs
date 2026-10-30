using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMarket.DbProvider.Entities.Categories;
using WebMarket.DbProvider.Settings.Constants;

namespace WebMarket.DbProvider.Configurations;

/// <summary>
/// Маппинг категории на catalog.categories
/// </summary>
internal sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> entity)
    {
        entity.HasKey(e => e.Id).HasName("pk_categories");
        entity.ToTable("categories", DataConstants.Schemas.Catalog);

        entity.Property(e => e.Id).HasColumnName("id");
        entity.Property(e => e.ParentId).HasColumnName("parent_id");
        entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(128);
        entity.Property(e => e.Slug).HasColumnName("slug").HasMaxLength(128);
        entity.Property(e => e.SortOrder).HasColumnName("sort_order");

        entity.HasOne(e => e.Parent)
              .WithMany(e => e.Children)
              .HasForeignKey(e => e.ParentId)
              .HasConstraintName("fk_categories_parent_id")
              .OnDelete(DeleteBehavior.Restrict);
    }
}
