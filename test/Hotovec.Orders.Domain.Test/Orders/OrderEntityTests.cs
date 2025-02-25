using Hotovec.Orders.Domain.Common.Exceptions;
using Hotovec.Orders.Domain.Orders;
using Hotovec.Orders.Domain.Orders.Dtos;
using Hotovec.Orders.Domain.Orders.MonetaryInformation;
using Hotovec.Orders.Domain.Orders.Snapshots;

namespace Hotovec.Orders.Domain.Test.Orders;

public sealed class OrderEntityTests
{
    [Fact]
    public void Ctor_Creates()
    {
        // Arrange
        var dateCreated = new DateTimeOffset(new DateTime(2020, 1, 1));
        var items = new []
        {
            new OrderItemDto
                { ProductName = "Product 1", Quantity = 12, UnitPrice = new Money(10, new Currency("JPY")) }
        };
        
        // Act
        var actual = new OrderEntity(new OrderNumber("ORDER_1"), "Joe Doe", new Currency("JPY"), dateCreated, items);
        
        // Assert
        actual.Should().NotBeNull();
    }
    
    [Fact]
    public void Ctor_SnapshotInput_Creates()
    {
        // Arrange
        const string currencyCode = "CZK";
        var dateCreated = new DateTimeOffset(new DateTime(2025, 1, 1)).ToUnixTimeMilliseconds();
        var orderItemSnapshot = new OrderItemSnapshot
        (
            Id: 1,
            ProductName: "ProductName 3",
            UnitPrice: new MoneySnapshot(12.34m, currencyCode),
            Quantity: 5
        );
        var orderSnapshot = new OrderSnapshot("ORDER_10", "Jack Black", dateCreated, currencyCode, orderItemSnapshot);
        
        // Act
        var actual = new OrderEntity(orderSnapshot);
        
        // Assert
        actual.Should().NotBeNull();
    }
    
    [Fact]
    public void Ctor_SnapshotInput_Creates2()
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
    public void Ctor_NoItems_ThrowsDomainException()
    {
        // Arrange
        var items = Array.Empty<OrderItemDto>();
        var orderNumber = new OrderNumber("ORDER_1");
        const string customerName = "Joe Doe";
        var currency = new Currency("JPY");
        var dateCreated = new DateTimeOffset(new DateTime(2021, 10, 10));

        // Act
        var actual = () =>
        {
            _ = new OrderEntity(orderNumber, customerName, currency, dateCreated, items);
        };
        
        // Assert
        actual.Should().Throw<DomainException>();
    }

    [Fact]
    public void Ctor_ItemWithIncompatibleCurrency_ThrowsDomainException()
    {
        // Arrange
        const string orderCurrency = "EUR";
        const string orderItemCurrency = "CZK";
        var currency = new Currency(orderCurrency);
        var dateCreated = new DateTimeOffset(new DateTime(2022, 10, 1));
        var items = new []
        {
            new OrderItemDto
                { ProductName = "Product 20", Quantity = 12, UnitPrice = new Money(10, new Currency(orderItemCurrency)) }
        };
        var orderNumber = new OrderNumber("ORDER_10");
        const string customerName = "John Doe";
        
        // Act
        var actual = () =>
        {
            _ = new OrderEntity(orderNumber, customerName, currency, dateCreated, items);
        };
        
        // Assert
        actual.Should().Throw<DomainException>();
    }
    
    [Fact]
    public void GetAllItems_CtorInput_ReturnsOrderItems()
    {
        // Arrange
        var currency = new Currency("JPY");
        const string productName1 = "Product 1";
        const int quantity1 = 2;
        const decimal amount1 = 20;
        const string productName2 = "Product 2";
        const int quantity2 = 3;
        const decimal amount2 = 100;
        var dateCreated = new DateTimeOffset(new DateTime(2023, 1, 10));
        var items = new []
        {
            new OrderItemDto
                { ProductName = productName1, Quantity = quantity1, UnitPrice = new Money(amount1, currency) },
            new OrderItemDto
                { ProductName = productName2, Quantity = quantity2, UnitPrice = new Money(amount2, currency) }
        };
        var expected = new []
        {
            new OrderItemDto
                { ProductName = productName1, Quantity = quantity1, UnitPrice = new Money(amount1, currency) },
            new OrderItemDto
                { ProductName = productName2, Quantity = quantity2, UnitPrice = new Money(amount2, currency) }
        };
        
        // Act
        var actual = new OrderEntity(new OrderNumber("ORDER_1"), "Joe Doe", currency, dateCreated, items)
            .GetAllItems().ToArray();
        
        // Assert
        actual.Should().NotBeNull();
        actual.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [InlineData("John Don", "John Don")]
    [InlineData("Joe Doe","Joe Doe")]
    public void ChangeCustomer_ValidInput_ReturnsCustomerName(string customerName, string expected)
    {
        // Arrange
        const string customerNameInit = "Unknown";
        var dateCreated = new DateTimeOffset(new DateTime(2024, 11, 10));
        var items = new []
        {
            new OrderItemDto
                { ProductName = "Product 1", Quantity = 12, UnitPrice = new Money(10, new Currency("JPY")) }
        };
        var order = new OrderEntity(new OrderNumber("ORDER_1"), customerNameInit, new Currency("JPY"), dateCreated, items);

        // Act
        order.ChangeCustomer(customerName);
        var actual = order.CustomerName;
        
        // Assert
        actual.Should().Be(expected);
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void ChangeCustomer_InvalidInput_ThrowsArgumentException(string? customerName)
    {
        // Arrange
        const string customerNameInit = "Unknown";
        var dateCreated = new DateTimeOffset(new DateTime(2023, 11, 11));
        var items = new []
        {
            new OrderItemDto
                { ProductName = "Product 1", Quantity = 12, UnitPrice = new Money(10, new Currency("JPY")) }
        };
        var order = new OrderEntity(new OrderNumber("ORDER_1"), customerNameInit, new Currency("JPY"), dateCreated, items);
        
        // Act
        var actual = () => order.ChangeCustomer(customerName);
        
        // Assert
        actual.Should().Throw<ArgumentException>();
    }
    
    [Fact]
    public void ToSnapshot_Object_ReturnsOrderSnapshot()
    {
        // Arrange
        const string orderId = "ORDER_10";
        const string customerName = "Jack Black";
        var date = new DateTimeOffset(new DateTime(2025, 03, 01));
        var dateCreated = date.ToUnixTimeMilliseconds();
        const string currencyCode = "CZK";
        const string productName = "ProductName2";
        const decimal amount = 12.3m;
        const int quantity = 4;
        var items = new []
        {
            new OrderItemDto
                { ProductName = productName, Quantity = quantity, UnitPrice = new Money(amount, new Currency(currencyCode)) }
        };
        var order = new OrderEntity(new OrderNumber(orderId), customerName, new Currency(currencyCode), date, items);
        var orderItemSnapshot = new OrderItemSnapshot
        (
            Id: 1,
            ProductName: productName,
            UnitPrice: new MoneySnapshot(amount, currencyCode),
            Quantity: quantity
        );
        var expected = new OrderSnapshot(orderId, customerName, dateCreated, currencyCode, orderItemSnapshot);

        // Act
        var actual = order.ToSnapshot();
        
        // Assert
        actual.Should().BeEquivalentTo(expected);
    }
}
