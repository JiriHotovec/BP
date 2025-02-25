using FluentAssertions;
using Hotovec.Orders.Domain.Orders.MonetaryInformation;
using Hotovec.Orders.Domain.Orders.Snapshots;
using Xunit;
using Xunit.Abstractions;

namespace Hotovec.Orders.Domain.Test.Orders.MonetaryInformation;

public sealed class MoneyTests(ITestOutputHelper _testOutputHelper)
{
    [Fact]
    public void Ctor_Creates()
    {
        // Arrange
        const decimal amount = 115.5M;
        var currency = new Currency("CZK");
        
        // Act
        var actual = new Money(amount, currency);
        
        // Assert
        actual.Should().NotBeNull();
    }
    
    [Fact]
    public void Ctor_NonRoundedValue_CreatesAndValuesMatch()
    {
        // Arrange
        const decimal amount = 315.0M;
        var currency = new Currency("JPY");

        // Act
        var actual = new Money(amount, currency);
        
        // Assert
        actual.Amount.Should().Be(amount);
        actual.Currency.Should().Be(new Currency("JPY"));
    }

    [Theory]
    [InlineData(0.5, 0.5)]
    [InlineData(0.504, 0.50)]
    [InlineData(0.505, 0.51)]
    [InlineData(1500.12345789, 1500.12)]
    public void Round_ValuesSubjectToRounding_CreatesCorrectRoundedValue(decimal value, decimal expected)
    {
        // Arrange
        var money = new Money(value, new Currency("EUR"));
        
        // Act
        var actual = money.Round();
        
        // Assert
        actual.Amount.Should().Be(expected);
    }

    [Fact]
    public void Ctor_ToString_ProducesExpectedStringInCorrectFormat()
    {
        // Arrange
        const decimal amount = 1657.215M;
        var currency = new Currency("EUR");
        
        // Act
        var actual = new Money(amount, currency);
        
        // Assert
        actual.ToString().Should().NotBeNullOrWhiteSpace();
    }
    
    [Fact]
    public void CompareTo_DifferentCurrencies_ThrowsArgumentException()
    {
        // Arrange
        var moneyLeft = new Money(1657.215M, new Currency("EUR"));
        var moneyRight = new Money(1657.215M, new Currency("USD"));
        
        // Act
        var actual = () => _ = moneyLeft.CompareTo(moneyRight);
        
        // Assert
        actual.Should()
            .Throw<ArgumentException>()
            .WithMessage("Unable to compare money with different currencies. Currencies:*");
    }
 
    [Fact]
    public void CompareTo_DifferentCurrencies_ThrowsArgumentException_GetMessageTechnique()
    {
        // Arrange
        var moneyLeft = new Money(1657.215M, new Currency("EUR"));
        var moneyRight = new Money(1657.215M, new Currency("USD"));

        // Act
        var actual = () =>
        {
            try
            {
                _ = moneyLeft.CompareTo(moneyRight);
            }
            catch (Exception e)
            {
                _testOutputHelper.WriteLine(e.ToString());
                
                throw;
            }
        };
        
        // Assert
        actual.Should()
            .Throw<ArgumentException>()
            .WithMessage("Unable to compare money with different currencies. Currencies:*");
    }
    
    [Theory]
    [InlineData(10.33, 20.0, -1)]
    [InlineData(10.50, 10.50, 0)]
    [InlineData(153.115, 10.50, 1)]
    public void CompareTo_SameCurrencies_ReturnsExpected(decimal left, decimal right, int expected)
    {
        // Arrange
        var moneyLeft = new Money(left, new Currency("EUR"));
        var monetRight = new Money(right, new Currency("EUR"));
        
        // Act
        var actual = moneyLeft.CompareTo(monetRight);
        
        // Assert
        actual.Should().Be(expected);
    }
    
    [Theory]
    [InlineData(0.0, 12.34)]
    [InlineData(100.01, 112.35)]
    [InlineData(-100.01, -87.67)]
    public void ArithmeticOperation_CountWithNumber_ReturnsMoneyObject(decimal value, decimal expectedValue)
    {
        // Arrange
        const decimal amount = 12.34M;
        var money = new Money(amount, new Currency("JPY"));
        var expected = new Money(expectedValue, money.Currency);
        
        // Act
        var actualRight = money + value;
        var actualLeft = value + money;
        
        // Assert
        actualRight.Should().Be(expected);
        actualLeft.Should().Be(expected);
    }
    
    [Theory]
    [InlineData(0.0, 1_234.56)]
    [InlineData(1_000.01, 2_234.57)]
    [InlineData(-10_000.01, -8_765.45)]
    public void ArithmeticOperation_CountWithMoneyObject_ReturnsMoneyObject(decimal value, decimal expectedValue)
    {
        // Arrange
        const decimal amount = 1_234.56M;
        var currency = new Currency("CZK");
        var moneyLeft = new Money(amount, currency);
        var moneyRight = new Money(value, currency);
        var expected = new Money(expectedValue, currency);
        
        // Act
        var actual = moneyLeft + moneyRight;
        
        // Assert
        actual.Should().Be(expected);
    }
    
    [Theory]
    [InlineData(0.0, 16_319.24)]
    [InlineData(123.45, 16_195.79)]
    [InlineData(16_419.24, -100.0)]
    [InlineData(-1.12, 16_320.36)]
    public void ArithmeticOperation_SubtractionWithNumberRight_ReturnsMoneyObject(decimal value, decimal expectedValue)
    {
        // Arrange
        const decimal amount = 16_319.24M;
        var money = new Money(amount, new Currency("CAD"));
        var expected = new Money(expectedValue, money.Currency);
        
        // Act
        var actual = money - value;
        
        // Assert
        actual.Should().Be(expected);
    }
    
    [Theory]
    [InlineData(0.0, -805.35)]
    [InlineData(1_313.23, 507.88)]
    [InlineData(123.45, -681.9)]
    public void ArithmeticOperation_SubtractionWithNumberLeft_ReturnsMoneyObject(decimal value, decimal expectedValue)
    {
        // Arrange
        const decimal amount = 805.35M;
        var money = new Money(amount, new Currency("PLN"));
        var expected = new Money(expectedValue, money.Currency);
        
        // Act
        var actual = value - money;
        
        // Assert
        actual.Should().Be(expected);
    }
    
    [Theory]
    [InlineData(0.0, 123.45)]
    [InlineData(15.43, 108.02)]
    [InlineData(234.56, -111.11)]
    public void ArithmeticOperation_SubtractionWithMoneyObject_ReturnsMoneyObject(decimal value, decimal expectedValue)
    {
        // Arrange
        const decimal amount = 123.45M;
        var currency = new Currency("LRD");
        var moneyLeft = new Money(amount, currency);
        var moneyRight = new Money(value, currency);
        var expected = new Money(expectedValue, currency);
        
        // Act
        var actual = moneyLeft - moneyRight;
        
        // Assert
        actual.Should().Be(expected);
    }
    
    [Theory]
    [InlineData(0.0, 0.0)]
    [InlineData(0.1, 1.234)]
    [InlineData(-0.1, -1.234)]
    [InlineData(10.0, 123.4)]
    public void ArithmeticOperation_MultipliedByNumber_ReturnsMoneyObject(decimal value, decimal expectedValue)
    {
        // Arrange
        const decimal amount = 12.34M;
        var money = new Money(amount, new Currency("JPY"));
        var expected = new Money(expectedValue, money.Currency);
        
        // Act
        var actualRight = money * value;
        var actualLeft = value * money;
        
        // Assert
        actualRight.Should().Be(expected);
        actualLeft.Should().Be(expected);
    }
    
    [Theory]
    [InlineData(0.0, 0.0)]
    [InlineData(0.2, 24.69)]
    [InlineData(-0.2, -24.69)]
    [InlineData(20.0, 2469.0)]
    public void ArithmeticOperation_MultipliedByMoneyObject_ReturnsMoneyObject(decimal value, decimal expectedValue)
    {
        // Arrange
        const decimal amount = 123.45M;
        var currency = new Currency("CZK");
        var moneyLeft = new Money(amount, currency);
        var moneyRight = new Money(value, currency);
        var expected = new Money(expectedValue, currency);
        
        // Act
        var actual = moneyLeft * moneyRight;
        
        // Assert
        actual.Should().Be(expected);
    }
    
    [Theory]
    [InlineData(0.1, 1_001.0)]
    [InlineData(-0.1, -1_001.0)]
    [InlineData(10.0, 10.01)]
    public void ArithmeticOperation_DividedByNumberRight_ReturnsMoneyObject(decimal value, decimal expectedValue)
    {
        // Arrange
        const decimal amount = 100.10M;
        var money = new Money(amount, new Currency("CAD"));
        var expected = new Money(expectedValue, money.Currency);
        
        // Act
        var actual = money / value;
        
        // Assert
        actual.Should().Be(expected);
    }
    
    [Theory]
    [InlineData(0.0, 0.0)]
    [InlineData(5.0, 2.5)]
    [InlineData(-1.1, -0.55)]
    public void ArithmeticOperation_DividedByNumberLeft_ReturnsMoneyObject(decimal value, decimal expectedValue)
    {
        // Arrange
        const decimal amount = 2.0M;
        var money = new Money(amount, new Currency("PLN"));
        var expected = new Money(expectedValue, money.Currency);
        
        // Act
        var actual = value / money;
        
        // Assert
        actual.Should().Be(expected);
    }
    
    [Theory]
    [InlineData(0.2, 20.5)]
    [InlineData(-0.2, -20.5)]
    [InlineData(20.0, 0.205)]
    public void ArithmeticOperation_DividedByMoneyObject_ReturnsMoneyObject(decimal value, decimal expectedValue)
    {
        // Arrange
        const decimal amount = 4.1M;
        var currency = new Currency("CZK");
        var moneyLeft = new Money(amount, currency);
        var moneyRight = new Money(value, currency);
        var expected = new Money(expectedValue, currency);
        
        // Act
        var actual = moneyLeft / moneyRight;
        
        // Assert
        actual.Should().Be(expected);
    }
    
    [Fact]
    public void FromSnapshot_ValidInput_ReturnsMoneyObject()
    {
        // Arrange
        const decimal amount = 2_345.67M;
        const string currencyCode = "CZK";
        var snapshot = new MoneySnapshot(amount, currencyCode);
        var expected = new Money(amount, new Currency(currencyCode));
        
        // Act
        var actual = Money.FromSnapshot(snapshot);
        
        // Assert
        actual.Should().Be(expected);
    }
    
    [Theory]
    [InlineData(2.0, null)]
    [InlineData(null, "USD")]
    [InlineData(null, null)]
    public void FromSnapshot_InvalidInput_ThrowsException(double? value, string? currencyCode)
    {
        // Arrange
        decimal? amount = value.HasValue ? Convert.ToDecimal(value.Value) : null;
        var snapshot = new MoneySnapshot(amount, currencyCode);
        
        // Act
        var actual = () => Money.FromSnapshot(snapshot);
        
        // Assert
        actual.Should().Throw<Exception>();
    }
    
    [Fact]
    public void ToSnapshot_ValidInput_ReturnsSnapshot()
    {
        // Arrange
        const decimal amount = 345.67M;
        const string currencyCode = "EUR";
        var money = new Money(amount, new Currency(currencyCode));
        var expected = new MoneySnapshot(amount, currencyCode);
        
        // Act
        var actual = money.ToSnapshot();
        
        // Assert
        actual.Should().Be(expected);
    }
}
