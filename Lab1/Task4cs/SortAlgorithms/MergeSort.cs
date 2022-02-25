namespace Task4cs.SortAlgorithms;

public class MergeSort
{
    public static void Sort(int[] array)
    {
        MergeSortStep(array, 0, array.Length - 1);
    }

    private static void Merge(int[] array, int minIndex, int middleIndex, int maxIndex)
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

        for (var i = right; i <= maxIndex; i++)
        {
            temp[ind] = array[i];
            ind++;
        }

        for (var i = left; i <= middleIndex; i++)
        {
            temp[ind] = array[i];
            ind++;
        }

        for (var i = 0; i < temp.Length; i++)
            array[minIndex + i] = temp[i];
    }

    private static int[] MergeSortStep(int[] array, int minIndex, int maxIndex)
    {
        if (minIndex >= maxIndex) return array;
        var middleIndex = (minIndex + maxIndex) / 2;
        MergeSortStep(array, minIndex, middleIndex);
        MergeSortStep(array, middleIndex + 1, maxIndex);
        Merge(array, minIndex, middleIndex, maxIndex);
        return array;
    }
}