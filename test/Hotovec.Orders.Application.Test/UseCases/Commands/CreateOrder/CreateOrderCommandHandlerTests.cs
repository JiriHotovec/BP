using System.ComponentModel;
using Hotovec.Orders.Application.Persistence;
using Hotovec.Orders.Application.UseCases.Commands.CreateOrder;
using Hotovec.Orders.Domain.Orders;
using Hotovec.Orders.Domain.Orders.MonetaryInformation;
using NSubstitute;
using ApplicationException = Hotovec.Orders.Application.Exceptions.ApplicationException;

namespace Hotovec.Orders.Application.Test.UseCases.Commands.CreateOrder;

[Category("unit")]
[ExcludeFromCodeCoverage(Justification = "Test class")]
public class CreateOrderCommandHandlerTests
{
    [Fact]
    public void ExecuteAsync_AlreadyExistedOrder_ThrowsApplicationException()
    {
        // Arrange
        var mRepository = Substitute.For<IOrderEntityRepository>();
        mRepository
            .ExistsAsync(Arg.Any<OrderNumber>())
            .Returns(true);
        var mFactory = Substitute.For<ICreateOrderCommandFactory>();
        var orderNumber = new OrderNumber("ORDER_10");
        var currency = new Currency("CZK");
        var command = new CreateOrderCommand(orderNumber, currency, "Jim Slim", [
            new CreateOrderItem(1, "Item 1", 1000, new Money(1000, currency)) ]);
        var handler = new CreateOrderCommandHandler(mRepository, mFactory);
        
        // Act
        var actual = async () => await handler.ExecuteAsync(command);
        
        // Assert
        actual.Should().ThrowAsync<ApplicationException>();
    }
}
