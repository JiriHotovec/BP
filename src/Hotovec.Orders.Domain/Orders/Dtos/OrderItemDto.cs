using Hotovec.Orders.Domain.Orders.MonetaryInformation;

namespace Hotovec.Orders.Domain.Orders.Dtos;

/// <summary>
/// Represents a data transfer object (DTO) for an order item.
/// </summary>
/// <remarks>
/// This class is designed to encapsulate data for an individual order item
/// and is primarily used for transferring data between different layers of the application.
/// </remarks>
/// <seealso cref="OrderEntity"/>
/// <seealso cref="OrderItemEntity"/>
/// <seealso cref="Money"/>
public sealed class OrderItemDto
{
    /// <summary>
    /// Gets or sets the name of the product associated with the order item.
    /// </summary>
    /// <remarks>
    /// This property represents the name of the product for the corresponding order item
    /// and is intended to provide an easily accessible and human-readable identifier for the product.
    /// </remarks>
    public string ProductName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the unit price of the order item.
    /// </summary>
    /// <remarks>
    /// Represents the price per single unit of the product in the order.
    /// The value is encapsulated within the <see cref="Money"/> type,
    /// which ensures accurate handling of currency and amount.
    /// </remarks>
    public Money UnitPrice { get; set; }

    /// <summary>
    /// Represents the quantity of items associated with an order.
    /// </summary>
    /// <remarks>
    /// This property defines the exact number of units for a given order item.
    /// It plays a key role in calculating the total cost of the order and ensuring accuracy
    /// in order fulfillment processes.
    /// </remarks>
    public int Quantity { get; set; }
}
