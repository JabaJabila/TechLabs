namespace Task4cs.SortAlgorithms
{
    public class QuickSort
    {
        private static int SplitParts(List<int> list, int minIndex, int maxIndex)
        {
            var baseEl = minIndex - 1;
            for (var i = minIndex; i < maxIndex; i++)
                if (list[i] < list[maxIndex])
                {
                    baseEl++;
                    (list[baseEl], list[i]) = (list[i], list[baseEl]);
                }

            baseEl++;
            (list[baseEl], list[maxIndex]) = (list[maxIndex], list[baseEl]);
            return baseEl;
        }

        private static List<int> QuickSortStep(List<int> list, int minIndex, int maxIndex)
        {
            if (minIndex >= maxIndex) return list;
            var pivotIndex = SplitParts(list, minIndex, maxIndex);
            QuickSortStep(list, minIndex, pivotIndex - 1);
            QuickSortStep(list, pivotIndex + 1, maxIndex);
            return list;
        }

        public static void Sort(List<int> list)
        {
            QuickSortStep(list, 0, list.Count - 1);
        }
    }
}
