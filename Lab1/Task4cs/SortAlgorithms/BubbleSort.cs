namespace Task4cs.SortAlgorithms
{
    public class BubbleSort
    {
        public static void Sort(List<int> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
                for (int j = 0; j < list.Count - i - 1; j++)
                    if (list[j] > list[j + 1])
                        (list[j], list[j + 1]) = (list[j + 1], list[j]);
        }
    }
}
