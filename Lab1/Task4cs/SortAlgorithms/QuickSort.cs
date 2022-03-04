namespace Task4cs.SortAlgorithms;

public class QuickSort
{
    public static void Sort(int[] array)
    {
        QuickSortStep(array, 0, array.Length - 1);
    }

    private static int SplitParts(int[] array, int minIndex, int maxIndex)
    {
        var baseEl = minIndex - 1;
        for (var i = minIndex; i < maxIndex; i++)
            if (array[i] < array[maxIndex])
            {
                baseEl++;
                (array[baseEl], array[i]) = (array[i], array[baseEl]);
            }

        baseEl++;
        (array[baseEl], array[maxIndex]) = (array[maxIndex], array[baseEl]);
        return baseEl;
    }

    private static int[] QuickSortStep(int[] array, int minIndex, int maxIndex)
    {
        if (minIndex >= maxIndex) return array;
        var pivotIndex = SplitParts(array, minIndex, maxIndex);
        QuickSortStep(array, minIndex, pivotIndex - 1);
        QuickSortStep(array, pivotIndex + 1, maxIndex);
        return array;
    }
}