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
        IEnumerable<PieceMovement> GetMoveablePosition(bool isExpanded = false);

        /// <summary>
        /// 指定座標(論理座標)に移動可能かどうかを返す.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        bool IsMoveable(PublicDataType.IntegerVector2 position);

        /// <summary>
        /// 駒のアルファベットでの名前を返す. 不要なら消すかも.
        /// </summary>
        Terminologies.PieceName PieceName { get; }

        /// <summary>
        /// 駒の論理座標を返す.
        /// </summary>
        PublicDataType.IntegerVector2 Position { get; }

        /// <summary>
        /// 駒の色を返す. パイグ将棋特有のAPIなので, 分離したい.
        /// </summary>
        /// <returns></returns>
        Terminologies.PieceColor Color { get; }

        /// <summary>
        /// 1ターンあたりの移動回数を返す. 通常は1回.
        /// </summary>
        int NumberOfMoves { get; }

        /// <summary>
        /// PieceMovementの取得を試みる.
        /// </summary>
        /// <param name="worldPosition">絶対論理座標を指定する.</param>
        /// <param name="pieceMovement"></param>
        /// <returns></returns>
        bool TryToGetPieceMovement(PublicDataType.IntegerVector2 worldPosition, out PieceMovement pieceMovement);

        /// <summary>
        /// PieceMovementの取得を試みる.
        /// </summary>
        /// <param name="relativePosition">駒を中心とした相対論理座標を指定する.</param>
        /// <param name="pieceMovement"></param>
        /// <returns></returns>
        bool TryToGetPieceMovementByRelativePosition(PublicDataType.IntegerVector2 relativePosition, out PieceMovement pieceMovement);

        /// <summary>
        /// 引数のプレイヤーがオーナーであるかを返す
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        bool IsOwner(IPlayer player);

        /// <summary>
        /// この駒は取られることがあるかを返す.
        /// </summary>
        /// <returns></returns>
        bool IsCapturable();

        /// <summary>
        /// 入水判定が必要あるかを返す.
        /// </summary>
        /// <returns></returns>
        bool CanLittuaWithoutJudge();

        /// <summary>
        /// ほかの駒を取る能力があるかを返す.
        /// </summary>
        /// <returns></returns>
        bool CanTakePiece();
    }
}