using WebMarket.DbProvider.Entities.Categories;
using WebMarket.DbProvider.Entities.Owners;
using WebMarket.DbProvider.Entities.Products.Enums;
using WebMarket.DbProvider.Entities.Users;

namespace WebMarket.DbProvider.Entities.Products;

/// <summary>
/// Товар.
/// </summary>
public class Product
{
    public Guid Id { get; set; }

    public Guid OwnerId { get; set; }

    public Guid CategoryId { get; set; }

    public required string Sku { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public string Currency { get; set; } = null!; // Если не задана, берётся default БД

    public int Stock { get; set; }

    public int ReservedStock { get; set; }

    public ProductStatus Status { get; set; }

    public string Attributes { get; set; } = null!; // jsonb-объект, если не задан, берётся default БД

    public bool IsDeleted { get; set; }

    public Guid CreatedById { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public uint Version { get; set; }

    // Навигационные свойства
    public Owner Owner { get; set; } = null!;

    public Category Category { get; set; } = null!;

    public User CreatedBy { get; set; } = null!;

    public ICollection<ProductImage> Images { get; set; } = [];
}
