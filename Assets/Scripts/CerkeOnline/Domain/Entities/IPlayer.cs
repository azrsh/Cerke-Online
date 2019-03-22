using System;
using System.Collections.Generic;
using UniRx;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    public interface IPlayer
    {
        /// <summary>
        /// Playerが盤外に所持している駒が変更されたとき呼ばれる.
        /// </summary>
        IObservable<Unit> OnPieceStrageCahnged { get; }

        /// <summary>
        /// Playerが盤外に所持している駒のリストを取り出す.
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<IReadOnlyPiece> GetPieceList();

        /// <summary>
        /// Playerに駒を取得させる.
        /// </summary>
        /// <param name="piece"></param>
        void GivePiece(IPiece piece);

        /// <summary>
        /// Playerが盤外に所持している駒を取りだす.
        /// </summary>
        /// <param name="piece"></param>
        void PickOut(IPiece piece);
    }
}