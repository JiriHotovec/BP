namespace Hotovec.Orders.Domain.Orders.Snapshots;

public sealed record OrderItemSnapshot(int Id, string? ProductName, MoneySnapshot? UnitPrice, int Quantity);
