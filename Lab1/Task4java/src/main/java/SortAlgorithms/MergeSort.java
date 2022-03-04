package SortAlgorithms;

public class MergeSort implements ISort {
    public void sort(int[] array)
    {
        mergeSortStep(array, 0, array.length - 1);
    }

    @Override
    public String getSortName() {
        return "merge";
    }

    private static void merge(int[] array, int minIndex, int middleIndex, int maxIndex)
    {
        int left = minIndex;
        int right = middleIndex + 1;
        var temp = new int[maxIndex - minIndex + 1];
        int ind = 0;

        while (left <= middleIndex && right <= maxIndex)
        {
            if (array[left] < array[right])
            {
                temp[ind] = array[left];
                left++;
            }
            else
            {
                temp[ind] = array[right];
                right++;
            }
            ind++;
        }

        for (var i = left; i <= middleIndex; i++)
        {
            temp[ind] = array[i];
            ind++;
        }

        for (var i = right; i <= maxIndex; i++)
        {
            temp[ind] = array[i];
            ind++;
        }

        for (var i = 0; i < temp.length; i++)
            array[minIndex + i] = temp[i];
    }

    private static int[] mergeSortStep(int[] array, int minIndex, int maxIndex)
    {
        if (minIndex >= maxIndex) return array;
        var middleIndex = (minIndex + maxIndex) / 2;
        mergeSortStep(array, minIndex, middleIndex);
        mergeSortStep(array, middleIndex + 1, maxIndex);
        merge(array, minIndex, middleIndex, maxIndex);
        return array;
    }
}
