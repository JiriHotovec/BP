using System.ComponentModel.DataAnnotations;

namespace Hotovec.Orders.Api.Controllers.Orders.DeleteOrder;

public sealed record DeleteOrderRequest([Required] string OrderNumber);
