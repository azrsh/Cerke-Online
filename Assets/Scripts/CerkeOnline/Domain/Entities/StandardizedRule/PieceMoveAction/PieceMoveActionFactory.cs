using System;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;
using Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.PieceMoveAction.DataStructure;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.PieceMoveAction
{
    internal class PieceMoveActionFactory : IPieceMoveActionFactory
    {
        //PositionArrayAccessor<IPiece> pieces, IFieldEffectChecker fieldEffectCheckerをコンストラクタの引数にすることも検討
        public IPieceMoveAction Create(IPlayer player, PublicDataType.IntegerVector2 startPosition, PublicDataType.IntegerVector2 viaPosition, PublicDataType.IntegerVector2 endPosition,
            PositionArrayAccessor<IPiece> pieces, IFieldEffectChecker fieldEffectChecker, IValueInputProvider<int> valueProvider,
            PieceMovement start2ViaPieceMovement, PieceMovement via2EndPieceMovement, bool isTurnEnd)
        {
            var worldPath = WorldPathCalculator.CalculatePath(startPosition, viaPosition, endPosition, pieces, start2ViaPieceMovement, via2EndPieceMovement);
            var viaPositionNode = worldPath.Find(new ColumnData(viaPosition, pieces));  //経由点を通過するのが1回だけだということは保証されている.

            //worldPathに開始地点は含まれない
            var moveActionData = new MoveActionData(pieces.Read(startPosition), player, worldPath, viaPositionNode);
            
            return new PieceMoveAction(moveActionData,
                pieces, fieldEffectChecker, valueProvider, start2ViaPieceMovement, via2EndPieceMovement, isTurnEnd);
        }
    }
}
