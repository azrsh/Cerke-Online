using UnityEngine;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    public interface IPiece : IReadOnlyPiece
    {
        /// <summary>
        /// 指定座標（論理座標）に駒の座標を変更する.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        bool MoveTo(Vector2Int position, bool isForceMove = false);

        /// <summary>
        /// 駒の所有者を変更する. この情報は駒が持つべきではない気もする.
        /// </summary>
        /// <param name="owner"></param>
        void SetOwner(IPlayer owner);

        /// <summary>
        /// 駒をボード上から取り出したとき呼ばれる.
        /// </summary>
        bool PickUpFromBoard();

        /// <summary>
        /// ボードの外から駒をボード上に配置する.
        /// </summary>
        /// <param name="position"></param>
        void SetOnBoard(Vector2Int position);
    }
}