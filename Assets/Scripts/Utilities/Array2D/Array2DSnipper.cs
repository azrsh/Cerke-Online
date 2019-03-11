using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Azarashi.Utilities.Array2D
{
    public class Array2DSnipper<T>
    {
        readonly T[,] array;

        public Array2DSnipper(T[,] array)
        {
            this.array = array;
        }

        public T[,] Snip(Vector2Int start, Vector2Int end)
        {
            Vector2Int snippingVector = end - start;
            T[,] temp = new T[snippingVector.x, snippingVector.y];
            
            for (int j = 0; j < snippingVector.x; j++)
            {
                for (int k = 0; k < snippingVector.y; k++)
                {
                    temp[j, k] = array[start.x + j, start.y + k];
                }
            }

            return temp;
        }
    }
}