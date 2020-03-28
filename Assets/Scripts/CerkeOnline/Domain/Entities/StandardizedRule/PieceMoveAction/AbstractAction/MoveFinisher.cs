using System.Collections.Generic;
using System;
using UnityEngine;
using Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.PieceMoveAction.DataStructure;
using Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.PieceMoveAction.ActualAction;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.PieceMoveAction.AbstractAction
{
    internal class MoveFinisher
    {
        readonly Mover mover;
        readonly Capturer capturer;

        public MoveFinisher(Mover mover, Capturer capturer)
        {
            this.mover = mover;
            this.capturer = capturer;
        }

        public bool CheckIfContinuable(IPlayer player, IPiece movingPiece, LinkedListNode<ColumnData> worldPathNode, Action<PieceMoveResult> callback, Action onFailure, bool isTurnEnd)
        {
            var nextPiece = worldPathNode.Value.Piece;
            var nextPosition = worldPathNode.Value.Positin;
            var isLast = worldPathNode.Next == null;
            if (nextPiece != null)
            {
                if (capturer.IsCapturable(player, movingPiece, nextPiece) && isLast)
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
            IPiece gottenPiece = capturer.CapturePiece(player, movingPiece, endWorldPosition);
            mover.MovePiece(movingPiece, endWorldPosition, isForceMove);
            callback(new PieceMoveResult(true, isTurnEnd, gottenPiece));
        }
    }
}