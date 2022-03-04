package benchmark;

import SortAlgorithms.ISort;
import java.lang.management.ManagementFactory;
import java.lang.management.MemoryMXBean;
import java.util.Random;

public class MySortBenchmark {

    public int[] array;
    MemoryMXBean memoryMXBean = ManagementFactory.getMemoryMXBean();

    public void mySortBenchmark(ISort sortAlgo, int warmups, int iterations, int arraySize) {

        long[] res_t = new long[iterations];
        long[] res_mem = new long[iterations];
        array = new int[arraySize];

        // WARMING UP
        for (int i = 0; i < warmups; i++) {
            generateArray(arraySize);
            sortAlgo.sort(array);
        }

        // BENCHMARKING
        for (int i = 0; i < iterations; i++) {
            generateArray(arraySize);
            long mem_before = memoryMXBean.getHeapMemoryUsage().getUsed();
            long start_t = System.nanoTime();
            sortAlgo.sort(array);
            long end_t = System.nanoTime();
            long mem_after = memoryMXBean.getHeapMemoryUsage().getUsed();
            long diff_t = end_t - start_t;
            long diff_mem = mem_after - mem_before;
            System.out.printf("iter #%d: %s sort for int[%d] took %d nanoseconds\n",
                    i + 1, sortAlgo.getSortName(), arraySize, diff_t);

            res_t[i] = diff_t;
            res_mem[i] = diff_mem;
        }

        long mean_t = 0;
        long mean_mem = 0;
        int non_null_iters = iterations;
        for (int i = 0; i < iterations; i++) {
            mean_t += res_t[i];

            if (res_mem[i] <= 0) {
                non_null_iters--;
            }
            else {
                mean_mem += res_mem[i];
            }

        }

        mean_mem = mean_mem == 0 ? 0 : mean_mem / non_null_iters;

        // ESTIMATING MEAN
        System.out.printf(">> Mean for %s sort (int[%d]) = %.3f nanoseconds | allocated : %d bytes \n\n",
                sortAlgo.getSortName(), arraySize, (float)mean_t / iterations, mean_mem);
    }

    private void generateArray(int n) {
        Random rd = new Random();
        for (int i = 0; i < n; i++) {
            array[i] = rd.nextInt();
        }
    }
}
