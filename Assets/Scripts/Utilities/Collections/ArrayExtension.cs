using System;

namespace Azarashi.Utilities.Collections
{
    public static class ArrayExtension
    {
        public static void ForEach<T>(this T[] array, Func<int, T, T> func)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = func(i, array[i]);
        }

        public static void ForEach<T>(this T[,] array, Func<int, int, T, T> func)
        {
            for (int i = 0; i < array.GetLength(0); i++)
                for (int j = 0; j < array.GetLength(1); j++)
                    array[i, j] = func(i, j, array[i, j]);
        }

        public static void ForEach<T>(this T[,,] array, Func<int, int, int, T, T> func)
        {
            for (int i = 0; i < array.GetLength(0); i++)
                for (int j = 0; j < array.GetLength(1); j++)
                    for (int k = 0; k < array.GetLength(2); k++)
                        array[i, j, k] = func(i, j, k, array[i, j, k]);
        }



        public static T GetSafely<T>(this T[] array, int index, T defaultValue = default)
        {
            if (index < 0 || index >= array.Length)
                return defaultValue;

            return array[index];
        }

        public static T GetSafely<T>(this T[,] array, int index0, int index1, T defaultValue = default)
        {
            if (index0 < 0 || index0 >= array.GetLength(0) ||
                index1 < 0 || index1 >= array.GetLength(1))
                return defaultValue;

            return array[index0, index1];
        }

        public static T GetSafely<T>(this T[,,] array, int index0, int index1, int index2, T defaultValue = default)
        {
            if (index0 < 0 || index0 >= array.GetLength(0) ||
                index1 < 0 || index1 >= array.GetLength(1) ||
                index2 < 0 || index2 >= array.GetLength(2))
                return defaultValue;

            return array[index0, index1, index2];
        }

        public static bool SetSafely<T>(this T[] array, int index, T value)
        {
            if (index < 0 || index >= array.Length)
                return false;

            array[index] = value;
            return true;
        }

        public static bool SetSafely<T>(this T[,] array, int index0, int index1, T value)
        {
            if (index0 < 0 || index0 >= array.GetLength(0) ||
                index1 < 0 || index1 >= array.GetLength(1))
                return false;

            array[index0, index1] = value;
            return true;
        }

        public static bool SetSafely<T>(this T[,,] array, int index0, int index1, int index2, T value)
        {
            if (index0 < 0 || index0 >= array.GetLength(0) ||
                index1 < 0 || index1 >= array.GetLength(1) ||
                index2 < 0 || index2 >= array.GetLength(2))
                return false;

            array[index0, index1, index2] = value;
            return true;
        }
    }
}