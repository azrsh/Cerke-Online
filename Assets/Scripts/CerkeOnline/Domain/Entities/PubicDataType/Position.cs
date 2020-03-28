namespace Azarashi.CerkeOnline.Domain.Entities.PublicDataType
{
    public struct IntVector2
    {
        public static int Dot(IntVector2 right,IntVector2 left)
        {
            return right.x * left.x + right.y * left.y;
        }

        public int x { get; set; }
        public int y { get; set; }
        public int sqrMagnitude { get { return x * x + y * y; } }
        public IntVector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static IntVector2 operator +(IntVector2 left, IntVector2 right)
        {
            left.x += right.x;
            left.y += right.y;
            return left;
        }
        public static IntVector2 operator -(IntVector2 left, IntVector2 right)
        {
            left.x -= right.x;
            left.y -= right.y;
            return left;
        }
        public static bool operator ==(IntVector2 left, IntVector2 right)
        {
            return left.x == right.x && left.y == right.y;
        }
        public static bool operator !=(IntVector2 left, IntVector2 right)
        {
            return !(left == right);
        }
        public static IntVector2 operator *(IntVector2 left, int right)
        {
            left.x *= right;
            left.y *= right;
            return left;
        }
    }

    internal class PositionArrayAccessor<T>
    {
        readonly T[,] array;
        public int Height { get { return array.GetLength(0); } }
        public int Width { get { return array.GetLength(1); } }

        public PositionArrayAccessor(T[,] array)
        {
            this.array = array;
        }

        public T Read(IntVector2 position)
        {
            return array[position.y, position.x];
        }

        public void Write(IntVector2 position, T item)
        {
            array[position.y, position.x] = item;
        }
    }
}