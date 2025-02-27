using Hotovec.Orders.Domain.Orders;
using Hotovec.Orders.Domain.Orders.MonetaryInformation;
using Hotovec.Orders.Domain.Orders.Snapshots;

namespace Hotovec.Orders.Domain.Test.Orders;

[Category("unit")]
public sealed class OrderItemEntityTests
{
    [Fact]
    public void Ctor_SnapshotInput_Creates()
    {
        // Arrange
        const int id = 2;
        const string productName = "ProductName2";
        const decimal amount = 12.3m;
        const string currencyCode = "CZK";
        const int quantity = 4;
        var snapshot = new OrderItemSnapshot
        (
            Id: id,
            ProductName: productName,
            UnitPrice: new MoneySnapshot(amount, currencyCode),
            Quantity: quantity
        );
        var expected = new OrderItemEntity(
            id,
            productName,
            new Money(amount, new Currency(currencyCode)),
            quantity);
        
        // Act
        var actual = new OrderItemEntity(snapshot);
        
        // Assert
        actual.Should().NotBeNull();
        actual.Should().BeEquivalentTo(expected);
    }
    
    [Fact]
    public void Ctor_ValidInput_Creates()
    {
        // Arrange
        const int id = 10;
        const string productName = "ProductName";
        var unitPrice = new Money(100.5m, new Currency("CZK"));
        const int quantity = 3;
        
        // Act
        var actual = new OrderItemEntity(id, productName, unitPrice, quantity);
        
        // Assert
        actual.Should().NotBeNull();
    }
    
    [Fact]
    public void Ctor_SnapshotInvalidInput_ThrowsArgumentException()
    {
        // Arrange
        OrderItemSnapshot? snapshot = null;
        
        // Act
        var actual = () => _ = new OrderItemEntity(snapshot);
        
        // Assert
        actual.Should().Throw<ArgumentException>();
    }
    
    [Theory]
    [InlineData(null, null)]
    [InlineData("ProductName", null)]
    [InlineData(null, 299)]
    public void Ctor_InvalidInput_ThrowsArgumentException(string? productName, int? amount)
    {
        // Arrange
        const int id = 10;
        var unitPrice = amount.HasValue
                ? new Money(amount.Value, new Currency("CZK"))
                : null;
        const int quantity = 3;
        
        // Act
        var actual = () =>
            _ = new OrderItemEntity(id, productName, unitPrice, quantity);
        
        // Assert
        actual.Should().Throw<ArgumentException>();
    }
    
    [Fact]
    public void ToSnapshot_Object_ReturnsSnapshot()
    {
        // Arrange
        const int id = 10;
        const string productName = "Some Product";
        const decimal amount = 123.4m;
        const string currencyCode = "EUR";
        const int quantity = 2;
        var orderItem = new OrderItemEntity(
            id,
            productName,
            new Money(amount, new Currency(currencyCode)),
            quantity);
        var expected = new OrderItemSnapshot
        (
            Id: id,
            ProductName: productName,
            UnitPrice: new MoneySnapshot(amount, currencyCode),
            Quantity: quantity
        );
        
        // Act
        var actual = orderItem.ToSnapshot();
        
        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [InlineData(0.0, 1, 0.0)]
    [InlineData(1.0, 1, 1.0)]
    [InlineData(-1.0, 1, -1.0)]
    [InlineData(1_000.23, 4, 4_000.92)]
    public void TotalPrice_Calculate_ReturnsMoneyObject(decimal amount, int quantity, decimal expectedTotalPrice)
    {
        // Arrange
        const int id = 10;
        const string productName = "Some Product";
        const string currencyCode = "EUR";
        var orderItem = new OrderItemEntity(
            id,
            productName,
            new Money(amount, new Currency(currencyCode)),
            quantity);
        var expected = new Money(expectedTotalPrice, new Currency(currencyCode));
        
        // Act
        var actual = orderItem.TotalPrice;

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }
}
