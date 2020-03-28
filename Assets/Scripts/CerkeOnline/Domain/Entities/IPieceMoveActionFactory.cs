using System;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;
using Azarashi.CerkeOnline.Domain.Entities.StandardizedRule;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    internal interface IPieceMoveActionFactory
    {
        IPieceMoveAction Create(IPlayer player, PublicDataType.IntVector2 startPosition, PublicDataType.IntVector2 viaPosition, PublicDataType.IntVector2 endPosition,
            PositionArrayAccessor<IPiece> pieces, IFieldEffectChecker fieldEffectChecker, IValueInputProvider<int> valueProvider,
            PieceMovement start2ViaPieceMovement, PieceMovement via2EndPieceMovement,
            Action<PieceMoveResult> callback, Action onPiecesChanged, bool isTurnEnd);
    }
}