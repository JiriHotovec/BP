using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Hotovec.Orders.Domain.Orders.MonetaryInformation;

namespace Hotovec.Orders.Test.Performance.Benchmark;

[SimpleJob(RuntimeMoniker.Net90)]
[RPlotExporter]
public class MoneyTests
{
    private int _priceLeftValue = 1;
    private const decimal PriceRightValue = 1.01m;
    private Money _priceLeft;
    private Money _priceRight;
    
    [Params(1000, 10000)]
    public int N;
    
    [GlobalSetup]
    public void Setup()
    {
        _priceLeft = new Money(_priceLeftValue, new Currency("CZK"));
        _priceRight = new Money(PriceRightValue, new Currency("CZK"));
        _priceLeftValue = new Random(42).Next();
    }
    
    [Benchmark]
    public decimal OnlyDecimalCount() => _priceLeftValue + PriceRightValue;

    [Benchmark]
    public Money CountWithNumber() => _priceLeft + PriceRightValue;
    
    [Benchmark]
    public Money CountWithMoneyObject() => _priceLeft + _priceRight;
    
    [Benchmark]
    public Money MultipliedByNumber() => _priceLeft * PriceRightValue;
    
    [Benchmark]
    public Money MultipliedByMoneyObject() => _priceLeft * _priceRight;
}
