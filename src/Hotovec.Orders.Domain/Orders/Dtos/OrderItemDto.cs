using Hotovec.Orders.Domain.Orders.MonetaryInformation;

namespace Hotovec.Orders.Domain.Orders.Dtos;

public sealed class OrderItemDto
{
    public string ProductName { get; set; } = null!;

    public Money UnitPrice { get; set; }

    public int Quantity { get; set; }
}
