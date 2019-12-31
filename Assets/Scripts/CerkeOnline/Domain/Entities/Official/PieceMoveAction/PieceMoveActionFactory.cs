using System;
using UnityEngine;
using Azarashi.Utilities.Collections;
using Azarashi.CerkeOnline.Domain.Entities.Official.PieceMoveAction.DataStructure;

namespace Azarashi.CerkeOnline.Domain.Entities.Official.PieceMoveAction
{
    public class PieceMoveActionFactory : IPieceMoveActionFactory
    {
        //Vector2YXArrayAccessor<IPiece> pieces, IFieldEffectChecker fieldEffectCheckerをコンストラクタの引数にすることも検討
        public IPieceMoveAction Create(IPlayer player, Vector2Int startPosition, Vector2Int viaPosition, Vector2Int endPosition,
            Vector2YXArrayAccessor<IPiece> pieces, IFieldEffectChecker fieldEffectChecker, IValueInputProvider<int> valueProvider,
            PieceMovement start2ViaPieceMovement, PieceMovement via2EndPieceMovement, 
            Action<PieceMoveResult> callback, Action onPiecesChanged, bool isTurnEnd)
        {
            var worldPath = new PieceMovePathCalculator().CalculatePath(startPosition, viaPosition, endPosition, pieces, start2ViaPieceMovement, via2EndPieceMovement);
            var viaPositionNode = worldPath.Find(new ColumnData(viaPosition, pieces));
            var moveActionData = new MoveActionData(worldPath.First.Value.Piece, player, worldPath, viaPositionNode);
            return new PieceMoveAction(moveActionData,
                pieces, fieldEffectChecker, valueProvider, start2ViaPieceMovement, via2EndPieceMovement,
                callback, onPiecesChanged, isTurnEnd);
        }
    }
}
