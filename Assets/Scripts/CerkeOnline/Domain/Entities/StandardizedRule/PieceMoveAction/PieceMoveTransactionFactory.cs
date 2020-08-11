using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.PieceMoveAction
{
    internal class PieceMoveTransactionFactory : IPieceMoveTransactionFactory
    {
        //PositionArrayAccessor<IPiece> pieces, IFieldEffectChecker fieldEffectCheckerをコンストラクタの引数にすることも検討
        public IPieceMoveTransaction Create(IPlayer player, VerifiedMove verifiedMove, PositionArrayAccessor<IPiece> pieces, IFieldEffectChecker fieldEffectChecker, IValueInputProvider<int> valueProvider, bool isTurnEnd)
        {            
            return new PieceMoveTransaction(player, verifiedMove, pieces, fieldEffectChecker, valueProvider, isTurnEnd);
        }
    }
}
