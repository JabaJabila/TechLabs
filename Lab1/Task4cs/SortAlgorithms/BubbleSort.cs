﻿namespace Task4cs.SortAlgorithms;

public class BubbleSort
{
    public static void Sort(int[] array)
    {
        for (int i = 0; i < array.Length - 1; i++)
        for (int j = 0; j < array.Length - i - 1; j++)
            if (array[j] > array[j + 1])
            {
                (array[j], array[j + 1]) = (array[j + 1], array[j]);
            }
                        
    }
}