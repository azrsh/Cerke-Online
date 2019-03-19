using System;
using UnityEngine;
using UniRx;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    public interface IBoard
    {
        bool SetPiece(Vector2Int position, IPiece piece);
        void MovePiece(Vector2Int startPosition, Vector2Int endPosition, IPlayer player, IValueInputProvider<int> valueProvider, Action<PieceMoveResult> callback);
        IReadOnlyPiece GetPiece(Vector2Int position);
        IObservable<Unit> OnEveruValueChanged { get; }
    }
}