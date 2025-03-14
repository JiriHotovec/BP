using System.Diagnostics.CodeAnalysis;
using BenchmarkDotNet.Attributes;
using Hotovec.Orders.Domain.Orders.MonetaryInformation;

namespace Hotovec.Orders.Performance.Test;

[MemoryDiagnoser]
[ExcludeFromCodeCoverage(Justification = "Test class")]
public class Benchmarks
{
    /// <summary>
    ///     The number of iterations to perform in the benchmark tests.
    /// </summary>
    /// <remarks>
    ///     This property determines the number of times the benchmark loops will be executed.
    ///     It is used to assess the performance of operations with varying iteration counts.
    /// </remarks>
    [Params(10_000, 100_000, 1_000_000)]
    public int N { get; set; }

    [Benchmark]
    public void Money_OperatorAddition()
    {
        for (var i = 0; i < N; i++)
        {
            var a = new Money(MonetaryData.ValueLeft, Currencies.CurrencyEur );
            var b = new Money(MonetaryData.ValueRight, Currencies.CurrencyEur );
            _ = a + b;
        }
    }

    /// <summary>
    ///     Adds two decimal values together multiple times in a loop.
    /// </summary>
    /// <remarks>
    ///     This method benchmarks the efficiency of adding two simple decimal values
    ///     by performing the addition operation repeatedly for a specified number of iterations (N).
    ///     It serves as a baseline comparison for performance testing with other types such as
    ///     objects representing monetary values.
    /// </remarks>
    [Benchmark(Baseline = true)]
    public void AddingTwoSimpleDecimals()
    {
        for (var i = 0; i < N; i++) _ = MonetaryData.ValueLeft + MonetaryData.ValueRight;
    }

    private static class MonetaryData
    {
        public static decimal ValueLeft => 315_217.0m;
        public static decimal ValueRight => 457_156.0m;
    }

    /// <summary>
    ///     Static resources for test to avoid allocations
    /// </summary>
    private static class Currencies
    {
        public static Currency CurrencyEur => new ("EUR");
    }
}
