using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMarket.DbProvider.Entities.Products;
using WebMarket.DbProvider.Settings.Constants;

namespace WebMarket.DbProvider.Configurations;

/// <summary>
/// Маппинг товара на catalog.products.
/// </summary>
internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> entity)
    {
        entity.HasKey(e => e.Id).HasName("pk_products");
        entity.ToTable("products", DataConstants.Schemas.Catalog);
        entity.HasQueryFilter(DataConstants.SoftDeleteFilter, e => !e.IsDeleted);

        entity.Property(e => e.Id).HasColumnName("id");
        entity.Property(e => e.OwnerId).HasColumnName("owner_id");
        entity.Property(e => e.CategoryId).HasColumnName("category_id");
        entity.Property(e => e.Sku).HasColumnName("sku").HasMaxLength(64);
        entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(256);
        entity.Property(e => e.Description).HasColumnName("description");
        entity.Property(e => e.Price).HasColumnName("price").HasPrecision(18, 2);
        entity.Property(e => e.Currency).HasColumnName("currency").HasMaxLength(3).IsFixedLength().HasDefaultValueSql("'RUB'");
        entity.Property(e => e.Stock).HasColumnName("stock");
        entity.Property(e => e.ReservedStock).HasColumnName("reserved_stock");
        entity.Property(e => e.Status).HasColumnName("status").HasConversion<short>();
        entity.Property(e => e.Attributes).HasColumnName("attributes").HasColumnType("jsonb").HasDefaultValueSql("'{}'");
        entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
        entity.Property(e => e.CreatedById).HasColumnName("created_by");
        entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()");
        entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("now()");
        entity.Property(e => e.Version).HasColumnName("xmin").IsRowVersion();

        entity.HasOne(e => e.Owner)
              .WithMany(e => e.Products)
              .HasForeignKey(e => e.OwnerId)
              .HasConstraintName("fk_products_owner_id")
              .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(e => e.Category)
              .WithMany(e => e.Products)
              .HasForeignKey(e => e.CategoryId)
              .HasConstraintName("fk_products_category_id")
              .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(e => e.CreatedBy)
              .WithMany(e => e.CreatedProducts)
              .HasForeignKey(e => e.CreatedById)
              .HasConstraintName("fk_products_created_by")
              .OnDelete(DeleteBehavior.Restrict);
    }
}
