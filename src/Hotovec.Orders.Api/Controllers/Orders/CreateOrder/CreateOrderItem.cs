using System.ComponentModel.DataAnnotations;

namespace Hotovec.Orders.Api.Controllers.Orders.CreateOrder;

public sealed record CreateOrderItem(
    int Id,
    [Required] string Name,
    decimal Price,
    int Quantity);
