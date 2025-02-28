using Hotovec.Orders.Api.Controllers.Orders.CreateOrder;
using Hotovec.Orders.Api.Controllers.Orders.DeleteOrder;
using Hotovec.Orders.Api.Controllers.Orders.GetOrderById;
using Hotovec.Orders.Application.UseCases.Commands.CreateOrder;
using Hotovec.Orders.Application.UseCases.Commands.DeleteOrder;
using Hotovec.Orders.Application.UseCases.Queries.GetOrderById;
using Hotovec.Orders.Domain.Orders;
using Hotovec.Orders.Domain.Orders.MonetaryInformation;
using CreateOrderItem = Hotovec.Orders.Application.UseCases.Commands.CreateOrder.CreateOrderItem;

namespace Hotovec.Orders.Api.Extensions;

public static class ControllerExtensions
{
    public static GetOrderByIdQuery AsQuery(this GetOrderByIdRequest request) =>
        new(new OrderNumber(request.OrderNumber));
    
    public static GetOrderByIdResponse AsResponse(this GetOrderByIdResult result) =>
        new(result.Order!.Id.ToString());
    
    public static CreateOrderCommand AsCommand(this CreateOrderRequest request) =>
        new(new OrderNumber(request.OrderNumber),
            new Currency(request.CurrencyCode),
            request.CustomerName,
            request.Items.Select(item => new CreateOrderItem(
                item.Id,
                item.Name,
                item.Quantity,
                new Money(item.Price, new Currency(request.CurrencyCode))))
                .ToArray());

    public static DeleteOrderCommand AsCommand(this DeleteOrderRequest request) =>
        new(new OrderNumber(request.OrderNumber));
}
