using BenchmarkDotNet.Running;
using Task4cs.Benchmark;

namespace Task4cs;

internal class Program
{
    internal static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<SortBenchmark>();
    }
}