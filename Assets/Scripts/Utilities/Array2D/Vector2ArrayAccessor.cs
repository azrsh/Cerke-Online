using UnityEngine;

namespace Azarashi.Utilities.Array2D
{
    /// <summary>
    /// Vector2Intを利用して二次元配列にアクセスする. array[y, x].
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Vector2ArrayAccessor<T>
    {
        readonly T[,] array;
        public int Height { get{ return array.GetLength(0); } }
        public int Width { get{ return array.GetLength(1); } }

        public Vector2ArrayAccessor(T[,] array)
        {
            this.array = array;
        }

        public T Read(Vector2Int position)
        {
            return array[position.y, position.x];
        }

        public void Write(Vector2Int position, T item)
        {
            array[position.y, position.x] = item;
        }
    }
}