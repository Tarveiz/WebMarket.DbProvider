using WebMarket.DbProvider.Entities.Owners;
using WebMarket.DbProvider.Entities.Products;

namespace WebMarket.DbProvider.Entities.Users;

/// <summary>
/// Пользователь
/// </summary>
public class User
{
    public Guid Id { get; set; }

    public required string Email { get; set; }

    public required string DisplayName { get; set; }

    public DateTime CreatedAt { get; set; }

    // Навигационные свойства
    public ICollection<Owner> CreatedOwners { get; set; } = [];

    public ICollection<OwnerMember> OwnerMemberships { get; set; } = [];

    public ICollection<Product> CreatedProducts { get; set; } = [];
}
