using UnityEngine;

namespace Azarashi.Utilities.Collections
{
    //上のと下のを統合したい

    /// <summary>
    /// Vector2Intを利用して二次元配列にアクセスする. array[y, x].
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Vector2YXArrayAccessor<T>
    {
        readonly T[,] array;
        public int Height { get { return array.GetLength(0); } }
        public int Width { get { return array.GetLength(1); } }

        public Vector2YXArrayAccessor(T[,] array)
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

    /// <summary>
    /// Vector2Intを利用して二次元配列にアクセスする. array[x, y].
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Vector2XYArrayAccessor<T>
    {
        readonly T[,] array;
        public int Width { get { return array.GetLength(0); } }
        public int Height { get { return array.GetLength(1); } }
        
        public Vector2XYArrayAccessor(T[,] array)
        {
            this.array = array;
        }

        public T Read(Vector2Int position)
        {
            return array[position.x, position.y];
        }

        public void Write(Vector2Int position, T item)
        {
            array[position.x, position.y] = item;
        }
    }

}