using System;

namespace Azarashi.CerkeOnline.Domain.Entities.PublicDataType
{
    public struct IntegerVector2 : IEquatable<IntegerVector2>
    {
        #region StaticMethod
        public static int Dot(IntegerVector2 right,IntegerVector2 left)
        {
            return right.x * left.x + right.y * left.y;
        }
        #endregion


        public int x { get; }
        public int y { get; }
        public int sqrMagnitude { get { return x * x + y * y; } }
        public IntegerVector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public bool Equals(IntegerVector2 other)
        {
            return this.x == other.x && this.y == other.y;
        }





        #region OperatorDefinition
        public static bool operator ==(IntegerVector2 left, IntegerVector2 right) => left.Equals(right);
        public static bool operator !=(IntegerVector2 left, IntegerVector2 right) => !left.Equals(right);
        public static IntegerVector2 operator+(IntegerVector2 left, IntegerVector2 right)
        {
            return new IntegerVector2(left.x + right.x, left.y + right.y);
        }
        public static IntegerVector2 operator-(IntegerVector2 left, IntegerVector2 right)
        {
            return new IntegerVector2(left.x - right.x, left.y - right.y);
        }
        public static IntegerVector2 operator*(IntegerVector2 left, int right)
        {
            return new IntegerVector2(left.x * right, left.y * right);
        }


        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
                return false;
            
            return this.Equals((IntegerVector2)obj);
        }

        public override int GetHashCode()
        {
            return this.x * this.y;
        }
        #endregion

        public override string ToString()
        {
            return "(" + this.x + ", " + this.y + ")";
        }
    }

    /// <summary>
    /// アクセスはYX型
    /// </summary>
    /// <typeparam name="T"></typeparam>
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