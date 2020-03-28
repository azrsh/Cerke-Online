using UnityEngine;
using Azarashi.Utilities.Collections;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.PieceMoveAction.ActualAction
{
    internal class Capturer
    {
        readonly Vector2YXArrayAccessor<IPiece> pieces;

        public Capturer(Vector2YXArrayAccessor<IPiece> pieces)
        {
            this.pieces = pieces;
        }

        public bool IsCapturable(IPlayer player, IPiece movingPiece, IPiece targetPiece)
        {
            if (targetPiece == null) return false;

            bool canMovingPieceTakePiece = movingPiece.CanTakePiece();
            bool isPieceCapturable = targetPiece.IsCapturable();
            bool isSameOwner = targetPiece.Owner == player;
            return canMovingPieceTakePiece && isPieceCapturable && !isSameOwner;
        }

        public IPiece CapturePiece(IPlayer player, IPiece movingPiece, Vector2Int endWorldPosition)
        {
            IPiece originalPiece = pieces.Read(endWorldPosition);     //命名が分かりにくい. 行先にある駒.
            if (!IsCapturable(player ,movingPiece, originalPiece))
                return null;

            IPiece gottenPiece = originalPiece;
            if (!gottenPiece.CaptureFromBoard()) return null;
            gottenPiece.SetOwner(player);
            pieces.Write(endWorldPosition, null);
            return gottenPiece;
        }
    }
}