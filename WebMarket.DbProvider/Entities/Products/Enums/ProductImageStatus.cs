namespace WebMarket.DbProvider.Entities.Products.Enums;

/// <summary>
/// Статус изображения товара
/// </summary>
public enum ProductImageStatus : short
{
    Uploading,
    Processing,
    Ready,
    Failed
}
