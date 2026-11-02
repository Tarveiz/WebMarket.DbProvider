# WebMarket.DbProvider

[![NuGet](https://img.shields.io/nuget/v/WebMarket.DbProvider.svg)](https://www.nuget.org/packages/WebMarket.DbProvider)

> **Note.** This package is part of the WebMarket pet project and is published on nuget.org only to demonstrate how shared dependencies are organized across the project's microservices. In real commercial projects, internal libraries like this should be distributed through a private NuGet server inside the company's protected infrastructure, not through a public package source.

Data access library for the shared WebMarket PostgreSQL database via EF Core and Dapper: `WebMarketDbContext` with entities and configurations, `IDbExecutor` (functions, procedures, raw SQL) and the `PgSqlSecret` connection settings.

## Installation

Install the package from nuget.org using the .NET CLI:

```bash
dotnet add package WebMarket.DbProvider
```

Or simply find `WebMarket.DbProvider` in the NuGet Package Manager in Visual Studio and install it from there.

`Npgsql.EntityFrameworkCore.PostgreSQL` and `Dapper` come with it, no need to add them separately. To get updates, bump the package version.

## Configuration

Connection settings are read from the `PgSqlSecret` configuration section:

```json
{
  "PgSqlSecret": {
    "Host": "localhost",
    "Port": 5432,
    "Database": "webmarket",
    "Username": "webmarket",
    "Password": "<password>"
  }
}
```

Registration:

```csharp
services.AddWebMarketData(configuration.GetRequiredSection(nameof(PgSqlSecret)).Get<PgSqlSecret>()!);
```

Registers a single `NpgsqlDataSource` shared by EF Core and Dapper, `WebMarketDbContext` (pooled, `NoTracking` by default) and `IDbExecutor`.

## Usage

```csharp
// Read via Dapper
var products = await dbExecutor.QueryFunctionAsync<ProductResponse>(ProductRoutines.GetCatalog, new { OwnerId = ownerId }, ct);

// Write via EF Core
await context.Products.Where(p => p.Id == productId).ExecuteUpdateAsync(s => s.SetProperty(p => p.Status, ProductStatus.Archived), ct);

// Dapper inside an EF Core transaction
await using var transaction = await context.Database.BeginTransactionAsync(ct);
await context.GetDbExecutor().CallProcedureAsync(ProductRoutines.ReserveStock, new { ProductId = productId, Quantity = quantity }, ct);
await transaction.CommitAsync(ct);
```

`ProductResponse` and `ProductRoutines` are defined in the consuming service.

- Transactions are managed by EF Core only. `IDbExecutor` from DI runs each call on its own pooled connection (autocommit); `context.GetDbExecutor()` joins the current `DbContext` connection and transaction.
- Function and procedure names are validated as lowercase snake_case identifiers; values are always passed as parameters.

## Building the package locally

```bash
dotnet pack WebMarket.DbProvider/WebMarket.DbProvider.csproj -c Release -o ./artifacts
```

## Publishing

Pushing a `vX.Y.Z` tag runs the GitHub Actions workflow that packs the library and publishes it to nuget.org via Trusted Publishing.

## License

[MIT](https://github.com/Tarveiz/WebMarket.DbProvider/blob/dev/0.1/LICENSE)

---

# WebMarket.DbProvider (русская версия)

> **Примечание.** Пакет является частью пет-проекта WebMarket и опубликован на nuget.org только для того, чтобы показать, как в проекте организованы общие зависимости между микросервисами. В реальных коммерческих проектах такие внутренние библиотеки должны распространяться через закрытый NuGet-сервер в защищённой инфраструктуре компании, а не через общедоступный источник пакетов.

Библиотека доступа к общей БД PostgreSQL WebMarket через EF Core и Dapper: `WebMarketDbContext` с сущностями и конфигурациями, `IDbExecutor` (функции, процедуры, произвольный SQL), настройки подключения `PgSqlSecret`.

## Установка

Установить пакет с nuget.org можно через .NET CLI:

```bash
dotnet add package WebMarket.DbProvider
```

Или просто найти `WebMarket.DbProvider` в менеджере NuGet-пакетов в Visual Studio и установить оттуда.

`Npgsql.EntityFrameworkCore.PostgreSQL` и `Dapper` подключаются вместе с ним, добавлять их отдельно не нужно. Для получения изменений достаточно обновить версию пакета.

## Конфигурация

Параметры подключения читаются из секции конфигурации `PgSqlSecret`:

```json
{
  "PgSqlSecret": {
    "Host": "localhost",
    "Port": 5432,
    "Database": "webmarket",
    "Username": "webmarket",
    "Password": "<password>"
  }
}
```

Регистрация:

```csharp
services.AddWebMarketData(configuration.GetRequiredSection(nameof(PgSqlSecret)).Get<PgSqlSecret>()!);
```

Регистрирует один `NpgsqlDataSource` для EF Core и Dapper, `WebMarketDbContext` (pool, по умолчанию `NoTracking`) и `IDbExecutor`.

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

`ProductResponse` и `ProductRoutines` объявляются в сервисе, который использует пакет.

- Транзакциями управляет только EF Core. `IDbExecutor` из DI выполняет каждый вызов на своём соединении из пула (автокоммит); `context.GetDbExecutor()` работает на соединении и в текущей транзакции `DbContext`.
- Имена функций и процедур проверяются как идентификаторы в lowercase snake_case; значения всегда передаются параметрами.

## Локальная сборка пакета

```bash
dotnet pack WebMarket.DbProvider/WebMarket.DbProvider.csproj -c Release -o ./artifacts
```

## Публикация

Push тега `vX.Y.Z` запускает workflow GitHub Actions, который собирает пакет и публикует его на nuget.org через Trusted Publishing.

## Лицензия

[MIT](https://github.com/Tarveiz/WebMarket.DbProvider/blob/dev/0.1/LICENSE)
