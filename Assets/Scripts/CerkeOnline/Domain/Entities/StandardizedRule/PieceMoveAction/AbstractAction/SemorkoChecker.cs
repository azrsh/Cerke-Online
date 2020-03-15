using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.PieceMoveAction.ActualAction;
using Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.PieceMoveAction.DataStructure;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.PieceMoveAction.AbstractAction
{
    /// <summary>
    /// 踏み越えチェッククラス
    /// </summary>
    public class SemorkoChecker
    {
        readonly MoveFinisher moveFinisher;
        readonly Pickupper pickupper;
        readonly Mover mover;

        readonly Action<PieceMoveResult> finishCallback;

        public SemorkoChecker(MoveFinisher moveFinisher, Pickupper pickupper, Mover mover,
            Action<PieceMoveResult> finishCallback)
        {
            this.moveFinisher = moveFinisher;
            this.pickupper = pickupper;
            this.mover = mover;

            this.finishCallback = finishCallback;
        }

        public bool CheckSemorko(Vector2Int viaPosition,IPlayer player, IPiece movingPiece, LinkedListNode<ColumnData> worldPathNode, Action moveAfterNext, Action<IPiece> onFailure)
        {
            var nextPiece = worldPathNode.Value.Piece;
            var isViaPosition = worldPathNode.Value.Positin == viaPosition && worldPathNode.Next != null;
            if (!isViaPosition)
                return true;

            IPiece semorkoNextPiece = worldPathNode.Next.Value.Piece;
            Action semorkoAction = null;
            if (semorkoNextPiece == null)
            {
                semorkoAction = () =>
                {
                    mover.MovePiece(movingPiece, worldPathNode.Next.Value.Positin, true);
                    moveAfterNext?.Invoke();
                };
            }
            else if (worldPathNode.Next.Next == null && pickupper.IsPickupable(player, movingPiece, semorkoNextPiece))
            {
                semorkoAction = () =>
                {
                    moveFinisher.FinishMove(player, movingPiece, worldPathNode.Next.Value.Positin, finishCallback, true);
                };
            }

            if (nextPiece == null || semorkoAction == null)
            {
                onFailure(movingPiece);
                return false;
            }

            //Unsafe 踏み越えられた場合のイベント通知
            if (nextPiece is ISemorkoObserver)
                (nextPiece as ISemorkoObserver).OnSurmounted.OnNext(Unit.Default);

            semorkoAction();

            return false;
        }
    }
}