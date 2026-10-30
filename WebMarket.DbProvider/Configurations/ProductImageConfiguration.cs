using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMarket.DbProvider.Entities.Products;
using WebMarket.DbProvider.Settings.Constants;

namespace WebMarket.DbProvider.Configurations;

/// <summary>
/// Маппинг изображения товара на catalog.product_images
/// </summary>
internal sealed class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> entity)
    {
        entity.HasKey(e => e.Id).HasName("pk_product_images");
        entity.ToTable("product_images", DataConstants.Schemas.Catalog);

        entity.Property(e => e.Id).HasColumnName("id");
        entity.Property(e => e.ProductId).HasColumnName("product_id");
        entity.Property(e => e.StorageKey).HasColumnName("storage_key").HasMaxLength(512);
        entity.Property(e => e.PreviewStorageKey).HasColumnName("preview_storage_key").HasMaxLength(512);
        entity.Property(e => e.ContentType).HasColumnName("content_type").HasMaxLength(128);
        entity.Property(e => e.SizeBytes).HasColumnName("size_bytes");
        entity.Property(e => e.SortOrder).HasColumnName("sort_order");
        entity.Property(e => e.Status).HasColumnName("status").HasConversion<short>();
        entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()");

        entity.HasOne(e => e.Product)
              .WithMany(e => e.Images)
              .HasForeignKey(e => e.ProductId)
              .HasConstraintName("fk_product_images_product_id")
              .OnDelete(DeleteBehavior.Cascade);
    }
}
