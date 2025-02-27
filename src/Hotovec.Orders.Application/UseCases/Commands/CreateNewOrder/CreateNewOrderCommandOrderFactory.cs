using Hotovec.Orders.Domain.Orders;
using Hotovec.Orders.Domain.Orders.Dtos;

namespace Hotovec.Orders.Application.UseCases.Commands.CreateNewOrder;

public sealed class CreateNewOrderCommandOrderFactory : ICreateNewOrderCommandOrderFactory
{
    private readonly TimeProvider _timeProvider;

    public CreateNewOrderCommandOrderFactory(TimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
    }
    
    public OrderEntity Create(CreateNewOrderCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        
        var dateCreated = _timeProvider.GetUtcNow();
        var items = command.Items.Select(i => new OrderItemDto()
        {
            Quantity = i.Quantity,
            UnitPrice = i.UnitPrice,
            ProductName = i.Name
        }).ToArray();
        
        return new OrderEntity(command.OrderNumber, command.CustomeName, command.Currency, dateCreated, items);
    }
}
