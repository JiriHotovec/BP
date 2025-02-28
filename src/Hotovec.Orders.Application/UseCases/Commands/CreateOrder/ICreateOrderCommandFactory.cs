using Hotovec.Orders.Domain.Orders;

namespace Hotovec.Orders.Application.UseCases.Commands.CreateOrder;

public interface ICreateOrderCommandFactory
{
    OrderEntity Create(CreateOrderCommand command);
}
