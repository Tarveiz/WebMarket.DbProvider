using WebMarket.DbProvider.Entities.Owners.Enums;
using WebMarket.DbProvider.Entities.Products;
using WebMarket.DbProvider.Entities.Users;

namespace WebMarket.DbProvider.Entities.Owners;

/// <summary>
/// Владелец товаров (продавец или магазин)
/// </summary>
public class Owner
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required string Slug { get; set; }

    public OwnerStatus Status { get; set; }

    public Guid CreatedById { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    // Навигационные свойства
    public User CreatedBy { get; set; } = null!;

    public ICollection<OwnerMember> Members { get; set; } = [];

    public ICollection<Product> Products { get; set; } = [];
}
