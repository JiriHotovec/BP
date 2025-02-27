using Hotovec.Orders.Domain.Orders;
using Hotovec.Orders.Domain.Orders.MonetaryInformation;

namespace Hotovec.Orders.Application.UseCases.Commands.CreateNewOrder;

public sealed record CreateNewOrderCommand(
    OrderNumber OrderNumber,
    Currency Currency,
    string CustomeName,
    params CreateNewOrderItem[] Items);
