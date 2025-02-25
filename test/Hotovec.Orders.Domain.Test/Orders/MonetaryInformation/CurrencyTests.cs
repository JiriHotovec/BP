using Hotovec.Orders.Domain.Orders.MonetaryInformation;

namespace Hotovec.Orders.Domain.Test.Orders.MonetaryInformation;

public sealed class CurrencyTests
{
    [Theory]
    // Data are taken from specialized class implementing IEnumerable<object[]> interface
    [ClassData(typeof(CurrencyTestData))]
    public void Ctor_AllowedCurrency_Creates(string input)
    {
        // Arrange
        // Act
        var actual = new Currency(input);

        // Assert
        actual.Should().NotBeNull();
    }

    [Theory]
    [ClassData(typeof(CurrencyTestData))]
    public void Ctor_AllowedCurrency_CodeMatches(string input)
    {
        // Arrange
        // Act
        var actual = new Currency(input);
        
        // Assert
        actual.Code.Should().Be(input);
        actual.ToString().Should().Be(input);
    }

    [Fact]
    public void Ctor_DifferentUseCase_ToleratesAndCreates()
    {
        // Arrange
        // Act
        var actual = new Currency("eur"); // Not uppercase
        
        // Assert
        actual.Code.Should().Be("EUR");
    }

    [Theory]
    [InlineData("/t")]
    [InlineData(" ")]
    [InlineData("")]
    [InlineData(null)]
    public void Ctor_MissingInput_ThrowsArgumentException(string? input)
    {
        // Arrange
        // Act
        var actual = () => _ = new Currency(input!);
        
        // Assert
        actual.Should().Throw<ArgumentException>();
    }


    [Theory]
    [InlineData("USD", "USD", true)]
    [InlineData("USD", "usd", true)]
    [InlineData("EUR", "usd", false)]
    public void Equals_ReturnsExpectedValue(string left, string right, bool expected)
    {
        // Arrange
        var currencyLeft = new Currency(left);
        var currencyRight = new Currency(right);
        
        // Act
        var actual = currencyLeft.Equals(currencyRight);
        
        // Assert
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData("USD", "USD", true)]
    [InlineData("USD", "usd", true)]
    [InlineData("EUR", "usd", false)]
    public void EqualityOperator_ReturnsExpectedValue(string left, string right, bool expected)
    {
        // Arrange
        var currencyLeft = new Currency(left);
        var currencyRight = new Currency(right);
        
        // Act
        var actual = currencyLeft == currencyRight;
        
        // Assert
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData("USD", "USD", false)]
    [InlineData("USD", "usd", false)]
    [InlineData("EUR", "usd", true)]
    public void InequalityOperator_ReturnsExpectedValue(string left, string right, bool expected)
    {
        // Arrange
        var currencyLeft = new Currency(left);
        var currencyRight = new Currency(right);
        
        // Act
        var actual = currencyLeft != currencyRight;
        
        // Assert
        actual.Should().Be(expected);
    }
}
