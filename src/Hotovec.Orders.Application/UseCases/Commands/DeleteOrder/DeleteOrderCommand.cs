using Hotovec.Orders.Domain.Orders;

namespace Hotovec.Orders.Application.UseCases.Commands.DeleteOrder;

public sealed record DeleteOrderCommand(OrderNumber OrderNumber);
