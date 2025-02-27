using Hotovec.Orders.Domain.Orders;

namespace Hotovec.Orders.Application.UseCases.Commands.CreateNewOrder;

public interface ICreateNewOrderCommandFactory
{
    OrderEntity Create(CreateNewOrderCommand command);
}
