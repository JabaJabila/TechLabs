using BenchmarkDotNet.Attributes;

namespace Task4cs.Benchmark;

[MemoryDiagnoser]
public class SortBenchmark
{
    public int[] ArrayToBenchmark { get; set; }

    [Params(1000, 5000, 10000, 50000, 100000)]
    public int Length { get; set; }

    [GlobalSetup]
    public void ArrayAlloc()
    {
        ArrayToBenchmark = new int[Length];
    }

    [IterationSetup]
    public void ArrayGen()
    {
        var rnd = new Random(Guid.NewGuid().GetHashCode());
        for (int i = 0; i < Length; i++) ArrayToBenchmark[i] = rnd.Next();
    }
        
        
    [WarmupCount(1)]
    [IterationCount(3)]
    [Benchmark]
    public void BubbleSort()
    {
        SortAlgorithms.BubbleSort.Sort(ArrayToBenchmark);
    }

    [WarmupCount(1)]
    [Benchmark]
    public void MergeSort()
    {
        SortAlgorithms.MergeSort.Sort(ArrayToBenchmark);
    }

    [WarmupCount(1)]
    [Benchmark]
    public void QuickSort()
    {
        SortAlgorithms.QuickSort.Sort(ArrayToBenchmark);
    }

    [WarmupCount(1)]
    [Benchmark]
    public void StandardSort()
    {
        Array.Sort(ArrayToBenchmark);
    }
}