using System.Collections;
using UnityEngine;
using Azarashi.Utilities.Collections;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.PieceMoveAction.ActualAction
{
    public class Pickupper
    {
        readonly Vector2YXArrayAccessor<IPiece> pieces;

        public Pickupper(Vector2YXArrayAccessor<IPiece> pieces)
        {
            this.pieces = pieces;
        }

        public bool IsPickupable(IPlayer player, IPiece movingPiece, IPiece targetPiece)
        {
            if (targetPiece == null) return false;

            bool canMovingPieceTakePiece = movingPiece.CanTakePiece();
            bool isPiecePickupable = targetPiece.IsPickupable();
            bool isSameOwner = targetPiece.Owner == player;
            return canMovingPieceTakePiece && isPiecePickupable && !isSameOwner;
        }

        public IPiece PickUpPiece(IPlayer player, IPiece movingPiece, Vector2Int endWorldPosition)
        {
            IPiece originalPiece = pieces.Read(endWorldPosition);     //命名が分かりにくい. 行先にある駒.
            if (!IsPickupable(player ,movingPiece, originalPiece))
                return null;

            IPiece gottenPiece = originalPiece;
            if (!gottenPiece.PickUpFromBoard()) return null;
            gottenPiece.SetOwner(player);
            pieces.Write(endWorldPosition, null);
            return gottenPiece;
        }
    }
}