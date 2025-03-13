using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

namespace Hotovec.Orders.Performance.Test;

/// <summary>
/// Run the benchmark test by command 'dotnet run -c release'.
/// </summary>
public class Program
{
    public static void Main(string[] args)
    {
        var config = DefaultConfig.Instance;
        BenchmarkRunner.Run<Benchmarks>(config, args);
    }
}
