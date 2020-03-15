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
    public class SteppingChecker
    {
        readonly MoveFinisher moveFinisher;
        readonly Capturer capturer;
        readonly Mover mover;

        readonly Action<PieceMoveResult> finishCallback;

        public SteppingChecker(MoveFinisher moveFinisher, Capturer capturer, Mover mover,
            Action<PieceMoveResult> finishCallback)
        {
            this.moveFinisher = moveFinisher;
            this.capturer = capturer;
            this.mover = mover;

            this.finishCallback = finishCallback;
        }

        public bool CheckStepping(Vector2Int viaPosition,IPlayer player, IPiece movingPiece, LinkedListNode<ColumnData> worldPathNode, Action moveAfterNext, Action<IPiece> onFailure)
        {
            var nextPiece = worldPathNode.Value.Piece;
            var isViaPosition = worldPathNode.Value.Positin == viaPosition && worldPathNode.Next != null;
            if (!isViaPosition)
                return true;

            IPiece steppingNextPiece = worldPathNode.Next.Value.Piece;
            Action steppingAction = null;
            if (steppingNextPiece == null)
            {
                steppingAction = () =>
                {
                    mover.MovePiece(movingPiece, worldPathNode.Next.Value.Positin, true);
                    moveAfterNext?.Invoke();
                };
            }
            else if (worldPathNode.Next.Next == null && capturer.IsCapturable(player, movingPiece, steppingNextPiece))
            {
                steppingAction = () =>
                {
                    moveFinisher.FinishMove(player, movingPiece, worldPathNode.Next.Value.Positin, finishCallback, true);
                };
            }

            if (nextPiece == null || steppingAction == null)
            {
                onFailure(movingPiece);
                return false;
            }

            //Unsafe 踏み越えられた場合のイベント通知
            if (nextPiece is ISteppedObserver)
                (nextPiece as ISteppedObserver).OnSteppied.OnNext(Unit.Default);

            steppingAction();

            return false;
        }
    }
}