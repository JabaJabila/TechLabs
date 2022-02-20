using BenchmarkDotNet.Attributes;

namespace Task4cs.Benchmark
{
    [MemoryDiagnoser]
    public class SortBenchmark
    {
        private int _listLength;
        public List<int> ListToBenchmark { get; set; }

        [Params(100, 1000, 10000, 100000)]
        public int ListLength
        {
            get => _listLength;
            set
            {
                _listLength = value;
                ListToBenchmark = ListGen(value);
            }
        }

        [Benchmark]
        public void BubbleSort()
        {
            SortAlgorithms.BubbleSort.Sort(ListToBenchmark);
        }

        [Benchmark]
        public void MergeSort()
        {
            SortAlgorithms.MergeSort.Sort(ListToBenchmark);
        }

        [Benchmark]
        public void QuickSort()
        {
            SortAlgorithms.QuickSort.Sort(ListToBenchmark);
        }

        [Benchmark]
        public void StandardSort()
        {
            ListToBenchmark.Sort();
        }

        public static List<int> ListGen(int n)
        {
            var list = new List<int>();
            var rnd = new Random(Guid.NewGuid().GetHashCode());
            for (int i = 0; i < n; i++) list.Add(rnd.Next() % 100);
            return list;
        }
    }
}
