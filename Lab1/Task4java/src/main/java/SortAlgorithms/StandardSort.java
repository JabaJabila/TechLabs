package SortAlgorithms;

import java.util.Arrays;

public class StandardSort implements ISort {
    public void sort(int[] array) {
        Arrays.sort(array);
    }

    public String getSortName() {
        return "standard";
    }
}
