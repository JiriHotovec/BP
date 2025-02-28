using Hotovec.Orders.Domain.Orders;

namespace Hotovec.Orders.Application.UseCases.Queries.GetOrderById;

public sealed record GetOrderByIdResult(OrderEntity? Order);
