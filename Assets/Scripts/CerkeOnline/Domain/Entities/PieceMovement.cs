using System;
using System.Collections.Generic;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    /// <summary>
    /// 方向と距離の情報を保持する. distance = -1は無限扱い. directionが一つ飛ばしなら一つ飛ばしでしか動けない.
    /// </summary>
    public struct PieceMovement
    {
        public readonly bool surmountable;
        public readonly int numberOfMove;
        public readonly PublicDataType.IntegerVector2 direction;
        public readonly int distance;
        public readonly List<PublicDataType.IntegerVector2> calcuratedMoveablePositions;

        public static PieceMovement Default { get { return default; } }

        public PieceMovement(PublicDataType.IntegerVector2 direction, int distance, bool surmountable = false, int numberOfMove = 1)
        {
            this.direction = direction;
            this.distance = distance;
            this.surmountable = surmountable;
            this.numberOfMove = numberOfMove;
            calcuratedMoveablePositions = new List<PublicDataType.IntegerVector2>();
        }

        /// <summary>
        /// 指定座標まで移動可能かどうかを返す.
        /// </summary>
        /// <param name="relativePosition"></param>
        /// <returns></returns>
        public bool IsMoveable(PublicDataType.IntegerVector2 relativePosition)
        {
            if (!IsSameDirection(relativePosition,direction)) return false;
            if (relativePosition.sqrMagnitude < direction.sqrMagnitude) return false;
            if (distance == -1) return true;
            if (calcuratedMoveablePositions.Contains(relativePosition)) return true;

            for (int i = 1; i <= distance; i++)
            {
                PublicDataType.IntegerVector2 current = direction * i;
                if (current == relativePosition)
                {
                    calcuratedMoveablePositions.Add(relativePosition);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 目標地点までの経路の座標が入った配列を返す. 開始地点は含まない. 目標地点は含む.
        /// </summary>
        /// <param name="relativePosition"></param>
        /// <returns></returns>
        public IEnumerable<PublicDataType.IntegerVector2> GetPath(PublicDataType.IntegerVector2 relativePosition)
        {
            if (!IsSameDirection(relativePosition, direction)) return null;
            if (relativePosition.sqrMagnitude < direction.sqrMagnitude) return null;
            List<PublicDataType.IntegerVector2> list = new List<PublicDataType.IntegerVector2>();
            for (int i = 1; i <= distance || distance == -1; i++)
            {
                PublicDataType.IntegerVector2 current = direction * i;
                list.Add(current);
                if (current == relativePosition)
                    return list;
            }
            return null;
        }

        bool IsSameDirection(PublicDataType.IntegerVector2 vector1, PublicDataType.IntegerVector2 vector2)
        {
            if ((vector1.x > 0) != (vector2.x > 0) || (vector1.y > 0) != (vector2.y > 0)) return false;
            var dot = PublicDataType.IntegerVector2.Dot(vector1, vector2);
            return dot * dot == vector1.sqrMagnitude * vector2.sqrMagnitude;
        }
    }
}