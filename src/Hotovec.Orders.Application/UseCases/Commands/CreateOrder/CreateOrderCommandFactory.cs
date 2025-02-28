using Hotovec.Orders.Domain.Orders;
using Hotovec.Orders.Domain.Orders.Dtos;

namespace Hotovec.Orders.Application.UseCases.Commands.CreateOrder;

public sealed class CreateOrderCommandFactory : ICreateOrderCommandFactory
{
    private readonly TimeProvider _timeProvider;

    public CreateOrderCommandFactory(TimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
    }
    
    public OrderEntity Create(CreateOrderCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        
        var dateCreated = _timeProvider.GetUtcNow();
        var items = command.Items.Select(i => new OrderItemDto()
        {
            Quantity = i.Quantity,
            UnitPrice = i.UnitPrice,
            ProductName = i.Name
        }).ToArray();
        
        return new OrderEntity(command.OrderNumber, command.CustomerName, command.Currency, dateCreated, items);
    }
}
