using Hotovec.Orders.Domain.Orders;

namespace Hotovec.Orders.Domain.Test.Orders;

[Category("unit")]
public sealed class OrderNumberTests
{
    [Fact]
    public void Ctor_Creates()
    {
        // Arrange
        const string orderNumber = "ORDER_0";
        
        // Act
        var actual = new OrderNumber(orderNumber);
        
        // Assert
        actual.Should().NotBeNull();
    }
    
    [Theory]
    [InlineData("ORDER_0", 0)]
    [InlineData("ORDER_05", 5)]
    [InlineData("ORDER_0010", 10)]
    public void Ctor_ValidInput_CreatesAndNumbersMatch(string orderNumber, int expectedOrderNumber)
    {
        // Arrange
        // Act
        var actual = new OrderNumber(orderNumber);
        
        // Assert
        actual.Number.Should().Be(expectedOrderNumber);
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("ORDER")]
    [InlineData("ORDER_")]
    [InlineData("ORDER_0X0")]
    [InlineData("ORDER_-1")]
    public void Ctor_InvalidInput_ThrowsArgumentException(string orderNumber)
    {
        // Arrange
        // Act
        var actual = () => _ = new OrderNumber(orderNumber);
        
        // Assert
        actual.Should().Throw<ArgumentException>();
    }
}
