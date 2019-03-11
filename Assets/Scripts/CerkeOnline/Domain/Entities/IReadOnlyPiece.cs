using System.Collections.Generic;
using UnityEngine;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    public interface IReadOnlyPiece
    {
        /// <summary>
        /// 駒の所有者を返す. この情報は駒が持つべきではない気もする. 循環参照になる.
        /// </summary>
        IPlayer Owner { get; }

        /// <summary>
        /// 移動可能な方向と距離に関する構造体のリストを返す.
        /// </summary>
        /// <param name="isExpanded"></param>
        /// <returns></returns>
        IReadOnlyList<PieceMovement> GetMoveablePosition(bool isExpanded = false);

        /// <summary>
        /// 指定座標(論理座標)に移動可能かどうかを返す.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        bool IsMoveable(Vector2Int position);

        /// <summary>
        /// 駒のアルファベットでの名前を返す. 不要なら消すかも.
        /// </summary>
        Terminologies.PieceName PieceName { get; }

        /// <summary>
        /// 駒の論理座標を返す.
        /// </summary>
        Vector2Int Position { get; }

        /// <summary>
        /// 駒の色を返す. パイグ将棋特有のAPIなので, 分離したい.
        /// </summary>
        /// <returns></returns>
        int Color { get; }


        bool TryToGetPieceMovement(Vector2Int position, out PieceMovement pieceMovement);
    }
}