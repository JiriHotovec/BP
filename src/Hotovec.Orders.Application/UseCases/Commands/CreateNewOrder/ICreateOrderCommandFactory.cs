using Hotovec.Orders.Domain.Orders;

namespace Hotovec.Orders.Application.UseCases.Commands.CreateNewOrder;

public interface ICreateOrderCommandFactory
{
    OrderEntity Create(CreateOrderCommand command);
}
