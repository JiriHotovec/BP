using Hotovec.Orders.Domain.Orders;

namespace Hotovec.Orders.Application.UseCases.Commands.CreateNewOrder;

public interface ICreateNewOrderCommandOrderFactory
{
    OrderEntity Create(CreateNewOrderCommand command);
}
