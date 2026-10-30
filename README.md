# WebMarket.DbProvider

Доступ к общей БД PostgreSQL WebMarket через EF Core и Dapper: `WebMarketDbContext` с сущностями и конфигурациями, `IDbExecutor` (функции, процедуры, произвольный SQL), секрет подключения `PgSqlSecret`.

## Подключение

```csharp
services.AddWebMarketData(configuration.GetRequiredSection(nameof(PgSqlSecret)).Get<PgSqlSecret>()!);
```

Регистрирует один `NpgsqlDataSource` для EF Core и Dapper, `WebMarketDbContext` (pool, `NoTracking`) и `IDbExecutor`.

## Использование

```csharp
// Чтение через Dapper
var products = await dbExecutor.QueryFunctionAsync<ProductResponse>(ProductRoutines.GetCatalog, new { OwnerId = ownerId }, ct);

// Запись через EF Core
await context.Products.Where(p => p.Id == productId).ExecuteUpdateAsync(s => s.SetProperty(p => p.Status, ProductStatus.Archived), ct);

// Dapper в транзакции EF Core
await using var transaction = await context.Database.BeginTransactionAsync(ct);
await context.GetDbExecutor().CallProcedureAsync(ProductRoutines.ReserveStock, new { ProductId = productId, Quantity = quantity }, ct);
await transaction.CommitAsync(ct);
```

## Сборка пакета

```bash
dotnet pack -c Release -p:Version=0.1.0
```
