using System.Collections.Generic;
using System;
using UnityEngine;
using Azarashi.CerkeOnline.Domain.Entities.Official.PieceMoveAction.DataStructure;
using Azarashi.CerkeOnline.Domain.Entities.Official.PieceMoveAction.ActualAction;

namespace Azarashi.CerkeOnline.Domain.Entities.Official.PieceMoveAction.AbstractAction
{
    public class MoveFinisher
    {
        readonly Mover mover;
        readonly Pickupper pickupper;

        public MoveFinisher(Mover mover, Pickupper pickupper)
        {
            this.mover = mover;
            this.pickupper = pickupper;
        }

        public bool CheckIfContinuable(IPlayer player, IPiece movingPiece, LinkedListNode<ColumnData> worldPathNode, Action<PieceMoveResult> callback, Action onFailure, bool isTurnEnd)
        {
            var nextPiece = worldPathNode.Value.Piece;
            var nextPosition = worldPathNode.Value.Positin;
            var isLast = worldPathNode.Next == null;
            if (nextPiece != null)
            {
                if (pickupper.IsPickupable(player, movingPiece, nextPiece) && isLast)
                {
                    FinishMove(player, movingPiece, nextPosition, callback, isTurnEnd);
                    return false;
                }

                //取ることが出ない駒が移動ルート上にある場合は移動失敗として終了する
                onFailure();
                return false;
            }

            return true;
        }


        public void FinishMove(IPlayer player, IPiece movingPiece, Vector2Int endWorldPosition, Action<PieceMoveResult> callback, bool isTurnEnd, bool isForceMove = false)
        {
            //移動先の駒を取る
            IPiece gottenPiece = pickupper.PickUpPiece(player, movingPiece, endWorldPosition);
            mover.MovePiece(movingPiece, endWorldPosition, isForceMove);
            callback(new PieceMoveResult(true, isTurnEnd, gottenPiece));
        }
    }
}