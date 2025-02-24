namespace Hotovec.Orders.Domain.Orders.Rounding;

/// <summary>
/// Provides a strategy for rounding decimal values to a specified number of decimal places using the AwayFromZero rounding method.
/// </summary>
/// <remarks>
/// This class is designed to standardize how decimal values are rounded throughout the application. It always rounds
/// to two decimal places with MidpointRounding.AwayFromZero, ensuring consistent behavior in financial calculations.
/// </remarks>
public static class DecimalRoundingStrategy 
{
    private static readonly int RoundToDecimals = 2;
    
    public static decimal Round(decimal value) =>
        decimal.Round(value, RoundToDecimals, MidpointRounding.AwayFromZero);
}
