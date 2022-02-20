namespace Task4cs.SortAlgorithms
{
    public class MergeSort
    {
        private static void Merge(List<int> list, int minIndex, int middleIndex, int maxIndex)
        {
            int left = minIndex;
            int right = middleIndex + 1;
            var temp = new int[maxIndex - minIndex + 1];
            int ind = 0;

            while (left <= middleIndex && right <= maxIndex)
            {
                if (list[left] < list[right])
                {
                    temp[ind] = list[left];
                    left++;
                }
                else
                {
                    temp[ind] = list[right];
                    right++;
                }
                ind++;
            }

            for (var i = left; i <= middleIndex; i++)
            {
                temp[ind] = list[i];
                ind++;
            }

            for (var i = right; i <= maxIndex; i++)
            {
                temp[ind] = list[i];
                ind++;
            }

            for (var i = 0; i < temp.Length; i++)
                list[minIndex + i] = temp[i];
        }

        private static List<int> MergeSortStep(List<int> list, int minIndex, int maxIndex)
        {
            if (minIndex >= maxIndex) return list;
            var middleIndex = (minIndex + maxIndex) / 2;
            MergeSortStep(list, minIndex, middleIndex);
            MergeSortStep(list, middleIndex + 1, maxIndex);
            Merge(list, minIndex, middleIndex, maxIndex);
            return list;
        }

        public static void Sort(List<int> list)
        {
            MergeSortStep(list, 0, list.Count - 1);
        }
    }
}
