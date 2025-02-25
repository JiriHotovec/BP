namespace Hotovec.Orders.Domain.Orders.Snapshots;

public sealed record MoneySnapshot(decimal? Amount, string? Currency);
