using Microsoft.EntityFrameworkCore;
using WebMarket.DbProvider.Entities.Categories;
using WebMarket.DbProvider.Entities.Owners;
using WebMarket.DbProvider.Entities.Products;
using WebMarket.DbProvider.Entities.Users;

namespace WebMarket.DbProvider;

/// <summary>
/// Контекст общей БД WebMarket
/// </summary>
public class WebMarketDbContext(DbContextOptions<WebMarketDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();

    public DbSet<Owner> Owners => Set<Owner>();

    public DbSet<OwnerMember> OwnerMembers => Set<OwnerMember>();

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<Product> Products => Set<Product>();

    public DbSet<ProductImage> ProductImages => Set<ProductImage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(typeof(WebMarketDbContext).Assembly);
}
