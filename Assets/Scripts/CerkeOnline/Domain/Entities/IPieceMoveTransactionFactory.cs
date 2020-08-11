using System;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;
using Azarashi.CerkeOnline.Domain.Entities.StandardizedRule;                    //読み込みをなくす
using Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.PieceMoveAction;    //読み込みをなくす

namespace Azarashi.CerkeOnline.Domain.Entities
{
    internal interface IPieceMoveTransactionFactory
    {
        IPieceMoveTransaction Create(IPlayer player, VerifiedMove verifiedMove, PositionArrayAccessor<IPiece> pieces, IFieldEffectChecker fieldEffectChecker, IValueInputProvider<int> valueProvider, bool isTurnEnd);
    }
}