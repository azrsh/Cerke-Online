using System.Collections.Generic;
using UnityEngine;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    /// <summary>
    /// 方向と距離の情報を保持する. distance = -1は無限扱い. directionが一つ飛ばしなら一つ飛ばしでしか動けない.
    /// </summary>
    public struct PieceMovement
    {
        public readonly bool surmountable;
        public readonly int numberOfMove;
        public readonly Vector2Int direction;
        public readonly int distance;
        public readonly List<Vector2Int> calcuratedMoveablePositions;

        public static PieceMovement Default { get { return default; } }

        public PieceMovement(Vector2Int direction, int distance, bool surmountable = false, int numberOfMove = 1)
        {
            this.direction = direction;
            this.distance = distance;
            this.surmountable = surmountable;
            this.numberOfMove = numberOfMove;
            calcuratedMoveablePositions = new List<Vector2Int>();
        }

        /// <summary>
        /// 指定座標まで移動可能かどうかを返す.
        /// </summary>
        /// <param name="relativePosition"></param>
        /// <returns></returns>
        public bool IsMoveable(Vector2Int relativePosition)
        {
            if (!IsSameDirection(relativePosition,direction)) return false;
            if (relativePosition.sqrMagnitude < direction.sqrMagnitude) return false;
            if (distance == -1) return true;
            if (calcuratedMoveablePositions.Contains(relativePosition)) return true;

            for (int i = 1; i <= distance; i++)
            {
                Vector2Int current = direction * i;
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
        public IReadOnlyList<Vector2Int> GetPath(Vector2Int relativePosition)
        {
            if (!IsSameDirection(relativePosition, direction)) return null;
            if (relativePosition.sqrMagnitude < direction.sqrMagnitude) return null;
            List<Vector2Int> list = new List<Vector2Int>();
            for (int i = 1; i <= distance || distance == -1; i++)
            {
                Vector2Int current = direction * i;
                list.Add(current);
                if (current == relativePosition)
                    return list;
            }
            return null;
        }

        bool IsSameDirection(Vector2Int vector1, Vector2Int vector2)
        {
            if ((vector1.x > 0) != (vector2.x > 0) || (vector1.y > 0) != (vector2.y > 0)) return false;
            return Mathf.Approximately(Vector2.Dot(vector1, vector2) * Vector2.Dot(vector1, vector2), vector1.sqrMagnitude * vector2.sqrMagnitude);
        }
    }
}