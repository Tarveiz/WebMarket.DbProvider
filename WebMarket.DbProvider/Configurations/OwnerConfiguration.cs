using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMarket.DbProvider.Entities.Owners;
using WebMarket.DbProvider.Settings.Constants;

namespace WebMarket.DbProvider.Configurations;

/// <summary>
/// Маппинг владельца на catalog.owners
/// </summary>
internal sealed class OwnerConfiguration : IEntityTypeConfiguration<Owner>
{
    public void Configure(EntityTypeBuilder<Owner> entity)
    {
        entity.HasKey(e => e.Id).HasName("pk_owners");
        entity.ToTable("owners", DataConstants.Schemas.Catalog);

        entity.Property(e => e.Id).HasColumnName("id");
        entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(256);
        entity.Property(e => e.Slug).HasColumnName("slug").HasMaxLength(128);
        entity.Property(e => e.Status).HasColumnName("status").HasConversion<short>();
        entity.Property(e => e.CreatedById).HasColumnName("created_by");
        entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()");
        entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("now()");

        entity.HasOne(e => e.CreatedBy)
              .WithMany(e => e.CreatedOwners)
              .HasForeignKey(e => e.CreatedById)
              .HasConstraintName("fk_owners_created_by")
              .OnDelete(DeleteBehavior.Restrict);
    }
}
