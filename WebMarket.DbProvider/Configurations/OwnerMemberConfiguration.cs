using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMarket.DbProvider.Entities.Owners;
using WebMarket.DbProvider.Settings.Constants;

namespace WebMarket.DbProvider.Configurations;

/// <summary>
/// Маппинг участника владельца на catalog.owner_members
/// </summary>
internal sealed class OwnerMemberConfiguration : IEntityTypeConfiguration<OwnerMember>
{
    public void Configure(EntityTypeBuilder<OwnerMember> entity)
    {
        entity.HasKey(e => new { e.OwnerId, e.UserId }).HasName("pk_owner_members");
        entity.ToTable("owner_members", DataConstants.Schemas.Catalog);

        entity.Property(e => e.OwnerId).HasColumnName("owner_id");
        entity.Property(e => e.UserId).HasColumnName("user_id");
        entity.Property(e => e.Role).HasColumnName("role").HasConversion<short>();
        entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()");

        entity.HasOne(e => e.Owner)
              .WithMany(e => e.Members)
              .HasForeignKey(e => e.OwnerId)
              .HasConstraintName("fk_owner_members_owner_id")
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.User)
              .WithMany(e => e.OwnerMemberships)
              .HasForeignKey(e => e.UserId)
              .HasConstraintName("fk_owner_members_user_id")
              .OnDelete(DeleteBehavior.Cascade);
    }
}
