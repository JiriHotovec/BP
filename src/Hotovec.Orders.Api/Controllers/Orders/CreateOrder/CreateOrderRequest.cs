using System.ComponentModel.DataAnnotations;

namespace Hotovec.Orders.Api.Controllers.Orders.CreateOrder;

public sealed record CreateOrderRequest(
    [Required] string OrderNumber,
    [Required] string CustomerName,
    [Required] string CurrencyCode,
    List<CreateOrderItem> Items);
