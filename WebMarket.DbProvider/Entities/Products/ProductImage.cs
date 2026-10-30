using WebMarket.DbProvider.Entities.Products.Enums;

namespace WebMarket.DbProvider.Entities.Products;

/// <summary>
/// Изображение товара
/// </summary>
public class ProductImage
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public required string StorageKey { get; set; }

    public string? PreviewStorageKey { get; set; }

    public required string ContentType { get; set; }

    public long SizeBytes { get; set; }

    public int SortOrder { get; set; }

    public ProductImageStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    // Навигационные свойства
    public Product Product { get; set; } = null!;
}
