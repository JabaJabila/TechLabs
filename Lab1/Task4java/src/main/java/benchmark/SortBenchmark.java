package benchmark;

import SortAlgorithms.BubbleSort;
import SortAlgorithms.MergeSort;
import org.openjdk.jmh.annotations.*;
import org.openjdk.jmh.infra.Blackhole;

import java.util.Arrays;
import java.util.Random;
import java.util.concurrent.TimeUnit;

public class SortBenchmark {

    @State(Scope.Benchmark)
    public static class ExecutionPlan {
        public int[] array;

        @Param({"1000", "5000", "10000", "50000", "100000"})
        public int length;

        public BubbleSort bubbleSort = new BubbleSort();
        public MergeSort mergeSort = new MergeSort();

        @Setup(Level.Iteration)
        public void generateArray() {
            Random rd = new Random();
            array = new int[length];
            for (int i = 0; i < length; i++) {
                array[i] = rd.nextInt();
            }
        }
    }

    @Benchmark
    @Fork(warmups = 0, value = 1)
    @Warmup(iterations = 1)
    @BenchmarkMode(Mode.AverageTime)
    @Measurement(iterations = 3)
    @OutputTimeUnit(TimeUnit.MICROSECONDS)
    public void bubbleSortBenchmark(ExecutionPlan plan) {
        plan.bubbleSort.sort(plan.array);
    }

    @Benchmark
    @Fork(warmups = 0, value = 1)
    @Warmup(iterations = 1)
    @BenchmarkMode(Mode.AverageTime)
    @Measurement(iterations = 10)
    @OutputTimeUnit(TimeUnit.MICROSECONDS)
    public void mergeSortBenchmark(ExecutionPlan plan, Blackhole blackhole) {
        plan.mergeSort.sort(plan.array);
        blackhole.consume(plan.array);
    }

    @Benchmark
    @Fork(warmups = 0, value = 1)
    @Warmup(iterations = 1)
    @BenchmarkMode(Mode.AverageTime)
    @Measurement(iterations = 10)
    @OutputTimeUnit(TimeUnit.MICROSECONDS)
    public void standardSortBenchmark(ExecutionPlan plan, Blackhole blackhole) {
        Arrays.sort(plan.array);
        blackhole.consume(plan.array);
    }

}
