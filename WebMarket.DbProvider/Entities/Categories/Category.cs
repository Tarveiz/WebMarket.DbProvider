using WebMarket.DbProvider.Entities.Products;

namespace WebMarket.DbProvider.Entities.Categories;

/// <summary>
/// Категория товаров
/// </summary>
public class Category
{
    public Guid Id { get; set; }

    public Guid? ParentId { get; set; }

    public required string Name { get; set; }

    public required string Slug { get; set; }

    public int SortOrder { get; set; }

    // Навигационные свойства
    public Category? Parent { get; set; }

    public ICollection<Category> Children { get; set; } = [];

    public ICollection<Product> Products { get; set; } = [];
}
