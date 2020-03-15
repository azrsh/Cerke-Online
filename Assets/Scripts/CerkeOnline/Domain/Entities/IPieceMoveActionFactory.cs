using System;
using UnityEngine;
using Azarashi.Utilities.Collections;
using Azarashi.CerkeOnline.Domain.Entities.StandardizedRule;
using Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.PieceMoveAction;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    public interface IPieceMoveActionFactory
    {
        IPieceMoveAction Create(IPlayer player, Vector2Int startPosition, Vector2Int viaPosition, Vector2Int endPosition,
            Vector2YXArrayAccessor<IPiece> pieces, IFieldEffectChecker fieldEffectChecker, IValueInputProvider<int> valueProvider,
            PieceMovement start2ViaPieceMovement, PieceMovement via2EndPieceMovement,
            Action<PieceMoveResult> callback, Action onPiecesChanged, bool isTurnEnd);
    }
}