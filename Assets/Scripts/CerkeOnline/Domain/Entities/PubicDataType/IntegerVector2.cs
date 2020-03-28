namespace Azarashi.CerkeOnline.Domain.Entities.PublicDataType
{
    public struct IntegerVector2
    {
        public static int Dot(IntegerVector2 right,IntegerVector2 left)
        {
            return right.x * left.x + right.y * left.y;
        }

        public int x { get; set; }
        public int y { get; set; }
        public int sqrMagnitude { get { return x * x + y * y; } }
        public IntegerVector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static IntegerVector2 operator +(IntegerVector2 left, IntegerVector2 right)
        {
            left.x += right.x;
            left.y += right.y;
            return left;
        }
        public static IntegerVector2 operator -(IntegerVector2 left, IntegerVector2 right)
        {
            left.x -= right.x;
            left.y -= right.y;
            return left;
        }
        public static bool operator ==(IntegerVector2 left, IntegerVector2 right)
        {
            return left.x == right.x && left.y == right.y;
        }
        public static bool operator !=(IntegerVector2 left, IntegerVector2 right)
        {
            return !(left == right);
        }
        public static IntegerVector2 operator *(IntegerVector2 left, int right)
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

        public T Read(IntegerVector2 position)
        {
            return array[position.y, position.x];
        }

        public void Write(IntegerVector2 position, T item)
        {
            array[position.y, position.x] = item;
        }
    }
}