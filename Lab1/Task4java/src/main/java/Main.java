import SortAlgorithms.BubbleSort;
import SortAlgorithms.MergeSort;
import SortAlgorithms.StandardSort;
import benchmark.MySortBenchmark;

public class Main {
    public static void main(String[] args)
    {
        var bench = new MySortBenchmark();
        int[] sizes = {1000, 5000, 10000, 50000, 100000};
        var standard = new StandardSort();
        var merge = new MergeSort();
        var bubble = new BubbleSort();

        for (int s : sizes) {
            bench.mySortBenchmark(bubble, 1, 10, s);
            bench.mySortBenchmark(merge, 1, 10, s);
            bench.mySortBenchmark(standard, 1, 10, s);
            System.out.println("---------------------------------------------\n");
        }
    }
}
