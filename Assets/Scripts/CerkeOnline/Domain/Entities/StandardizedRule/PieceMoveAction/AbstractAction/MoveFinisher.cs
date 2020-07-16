using System.Collections.Generic;
using System;
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

        public (PieceMoveResult pieceMoveResult, CaptureResult captureResult) ConfirmMove(IPlayer player, IPiece movingPiece, PublicDataType.IntegerVector2 endWorldPosition, bool isTurnEnd, bool isForceMove = false)
        {
            var captureResult = capturer.CapturePiece(player, movingPiece, endWorldPosition);
            if (!captureResult.IsSuccess)
                return (new PieceMoveResult(false, false, captureResult.Captured), captureResult);

            mover.MovePiece(movingPiece, endWorldPosition, isForceMove);
            return (new PieceMoveResult(true, isTurnEnd, captureResult.Captured), captureResult);
        }
    }
}