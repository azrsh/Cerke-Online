using System;
using UnityEngine;
using UniRx;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    public interface IBoard
    {
        /// <summary>
        /// 盤外の駒を盤上に設置する.
        /// </summary>
        /// <param name="position">設置位置</param>
        /// <param name="piece">設置する駒</param>
        /// <returns>成功時にtrue, 失敗時にfalse</returns>
        bool SetPiece(Vector2Int position, IPiece piece);

        /// <summary>
        /// 盤上の駒を指定された座標に移動する. 移動は引数に指定されたプレイヤーの権限で行われる.
        /// </summary>
        /// <param name="startPosition">開始位置</param>
        /// <param name="endPosition">目標位置</param>
        /// <param name="player">移動したプレイヤー</param>
        /// <param name="valueProvider">賽による判定値の提供者</param>
        /// <param name="callback">コールバック関数</param>
        void MovePiece(Vector2Int startPosition, Vector2Int endPosition, IPlayer player, IValueInputProvider<int> valueProvider, Action<PieceMoveResult> callback);

        /// <summary>
        /// 指定座標の駒のインスタンスを取得する.
        /// </summary>
        /// <param name="position">取得したい駒の座標</param>
        /// <returns></returns>
        IReadOnlyPiece GetPiece(Vector2Int position);

        /// <summary>
        /// 指定された種類の駒を検索する. 同種の駒が複数ある場合の動作は不定.
        /// </summary>
        /// <param name="pieceName"></param>
        /// <returns></returns>
        IReadOnlyPiece SearchPiece(Terminologies.PieceName pieceName);

        /// <summary>
        /// 盤の更新イベント.
        /// </summary>
        IObservable<Unit> OnEveruValueChanged { get; }
    }
}