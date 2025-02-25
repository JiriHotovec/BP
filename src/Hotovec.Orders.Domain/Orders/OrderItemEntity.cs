using Hotovec.Orders.Domain.Common.Entities;
using Hotovec.Orders.Domain.Orders.MonetaryInformation;
using Hotovec.Orders.Domain.Orders.Snapshots;

namespace Hotovec.Orders.Domain.Orders;

internal sealed class OrderItemEntity : Entity<int, OrderItemSnapshot>
{
    public OrderItemEntity(
        int id,
        string productName,
        Money unitPrice,
        int quantity)
    : base(id)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(productName);
        ArgumentNullException.ThrowIfNull(unitPrice);
        
        ProductName = productName;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }

    public OrderItemEntity(OrderItemSnapshot snapshot)
    : base(snapshot?.Id ?? 0)
    {
        ArgumentNullException.ThrowIfNull(snapshot);
        
        ProductName = snapshot.ProductName!;
        UnitPrice = Money.FromSnapshot(snapshot.UnitPrice!);
        Quantity = snapshot.Quantity;
    }

    public string ProductName { get; }

    public Money UnitPrice { get;  }

    public int Quantity { get; }

    public Money TotalPrice => UnitPrice * Quantity;

    public override string ToString() =>
        $"[OrderItem: Name: {ProductName}, UnitPrice: {UnitPrice}, Quantity: {Quantity}]";

    public override OrderItemSnapshot ToSnapshot() =>
        new(Id, ProductName, UnitPrice.ToSnapshot(), Quantity);
}
