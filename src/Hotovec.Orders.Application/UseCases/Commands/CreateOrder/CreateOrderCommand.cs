using Hotovec.Orders.Domain.Orders;
using Hotovec.Orders.Domain.Orders.MonetaryInformation;

namespace Hotovec.Orders.Application.UseCases.Commands.CreateOrder;

public sealed record CreateOrderCommand(
    OrderNumber OrderNumber,
    Currency Currency,
    string CustomerName,
    params CreateOrderItem[] Items);
