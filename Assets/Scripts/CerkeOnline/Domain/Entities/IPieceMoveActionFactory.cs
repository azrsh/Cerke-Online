using System;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;
using Azarashi.CerkeOnline.Domain.Entities.StandardizedRule;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    internal interface IPieceMoveActionFactory
    {
        IPieceMoveAction Create(IPlayer player, PublicDataType.IntegerVector2 startPosition, PublicDataType.IntegerVector2 viaPosition, PublicDataType.IntegerVector2 endPosition,
            PositionArrayAccessor<IPiece> pieces, IFieldEffectChecker fieldEffectChecker, IValueInputProvider<int> valueProvider,
            PieceMovement start2ViaPieceMovement, PieceMovement via2EndPieceMovement, bool isTurnEnd);
    }
}