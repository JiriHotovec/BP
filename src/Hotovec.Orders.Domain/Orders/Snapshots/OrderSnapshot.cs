namespace Hotovec.Orders.Domain.Orders.Snapshots;

public sealed record OrderSnapshot(
    string? Id,
    string? CustomerName,
    long? DateCreated,
    string? Currency,
    params OrderItemSnapshot[] Items);
