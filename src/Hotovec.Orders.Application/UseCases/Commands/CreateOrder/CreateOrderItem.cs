using Hotovec.Orders.Domain.Orders.MonetaryInformation;

namespace Hotovec.Orders.Application.UseCases.Commands.CreateOrder;

public sealed record CreateOrderItem(
    int Id,
    string Name,
    int Quantity,
    Money UnitPrice);
