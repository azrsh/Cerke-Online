using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.PieceMoveAction.ActualAction
{
    internal class Capturer
    {
        readonly PositionArrayAccessor<IPiece> pieces;

        public Capturer(PositionArrayAccessor<IPiece> pieces)
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

        public IPiece CapturePiece(IPlayer player, IPiece movingPiece, PublicDataType.IntegerVector2 endWorldPosition)
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