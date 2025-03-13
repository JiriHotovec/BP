namespace Hotovec.Orders.Domain.Orders.Rounding;

public static class DecimalRoundingStrategy 
{
    private static readonly int RoundToDecimals = 2;
    
    public static decimal Round(decimal value) =>
        decimal.Round(value, RoundToDecimals, MidpointRounding.AwayFromZero);
}
