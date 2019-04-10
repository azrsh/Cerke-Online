using System;
using System.Collections.Generic;
using Azarashi.CerkeOnline.Domain.Entities.Official.PieceMoveAction.DataStructure;
using Azarashi.CerkeOnline.Domain.Entities.Official.PieceMoveAction.ActualAction;

namespace Azarashi.CerkeOnline.Domain.Entities.Official.PieceMoveAction.AbstractAction
{
    public class PickupChecker
    {
        readonly Pickupper pickupper;
        readonly MoveFinisher moveFinisher;

        public void Check(IPlayer player, IPiece movingPiece, LinkedListNode<ColumnData> worldPathNode, Action<PieceMoveResult> callback, Action onFailure, bool isTurnEnd)
        {
            var nextPiece = worldPathNode.Value.Piece;
            var nextPosition = worldPathNode.Value.Positin;
            var isLast = worldPathNode.Next == null;
            if (nextPiece != null)
            {
                if (pickupper.IsPickupable(player, movingPiece, nextPiece) && isLast)
                {
                    moveFinisher.FinishMove(player, movingPiece, nextPosition, callback, isTurnEnd);
                    return;
                }

                //取ることが出ない駒が移動ルート上にある場合は移動失敗として終了する
                onFailure();
                return;
            }
        }
    }
}