using System;
using UnityEngine;
using Azarashi.Utilities.Collections;

namespace Azarashi.CerkeOnline.Domain.Entities.Official.PieceMoveAction.ActualAction
{
    public class Mover
    {
        readonly Vector2ArrayAccessor<IPiece> pieces;
        readonly Action onPiecesChanged;

        public Mover(Vector2ArrayAccessor<IPiece> pieces, Action onPiecesChanged)
        {
            this.pieces = pieces;
            this.onPiecesChanged = onPiecesChanged;
        }

        public void MovePiece(IPiece movingPiece, Vector2Int endWorldPosition, bool isForceMove = false)
        {
            Vector2Int startWorldPosition = movingPiece.Position;
            movingPiece.MoveTo(endWorldPosition, isForceMove);

            //この順で書きまないと現在いる座標と同じ座標をendWorldPositionに指定されたとき盤上から駒の判定がなくなる
            pieces.Write(startWorldPosition, null);
            pieces.Write(endWorldPosition, movingPiece);

            onPiecesChanged();
        }
    }
}