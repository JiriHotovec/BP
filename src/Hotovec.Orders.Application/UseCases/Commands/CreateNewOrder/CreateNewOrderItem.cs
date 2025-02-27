using Hotovec.Orders.Domain.Orders.MonetaryInformation;

namespace Hotovec.Orders.Application.UseCases.Commands.CreateNewOrder;

public sealed record CreateNewOrderItem(
    int Id,
    string Name,
    int Quantity,
    Money UnitPrice);
