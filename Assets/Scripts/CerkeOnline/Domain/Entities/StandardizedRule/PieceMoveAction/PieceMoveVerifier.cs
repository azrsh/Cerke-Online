using System.Linq;
using Azarashi.Utilities.Assertions;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.PieceMoveAction
{
    internal class PieceMoveVerifier
    {
        // long boardHash = 0;
        readonly IReadOnlyPositionArrayAccessor<IReadOnlyPiece> pieces;

        public PieceMoveVerifier(IReadOnlyPositionArrayAccessor<IReadOnlyPiece> pieces)
        {
            Assert.IsNotNull(pieces);

            this.pieces = pieces;
        }

        public VerifiedMove VerifyMove(IReadOnlyPlayer player, IntegerVector2 startPosition, IntegerVector2 viaPosition, IntegerVector2 endPosition)
        {
            Assert.IsTrue(IsOnBoard(startPosition));
            Assert.IsTrue(IsOnBoard(viaPosition) || viaPosition == new IntegerVector2(-1, -1));
            Assert.IsTrue(IsOnBoard(endPosition));

            //経路計算前の合法性チェック
            bool areViaAndLastSame = viaPosition == endPosition;
            IReadOnlyPiece movingPiece = pieces.Read(startPosition);
            IReadOnlyPiece viaPiece = pieces.Read(viaPosition);
            IReadOnlyPiece originalPiece = pieces.Read(endPosition);     //元からある駒の意味で使っているが, 英語があってるか不明.
            bool isTargetNull = movingPiece == null;
            bool isViaPieceNull = viaPiece == null;//
            bool isOwner = !isTargetNull && movingPiece.IsOwner(player);
            bool isSameOwner = !isTargetNull && originalPiece != null && originalPiece.Owner == movingPiece.Owner;
            PieceMovement start2ViaPieceMovement = PieceMovement.Default;
            PieceMovement via2EndPieceMovement = PieceMovement.Default;
            bool isMoveableToVia = !isTargetNull && movingPiece.TryToGetPieceMovement(viaPosition, out start2ViaPieceMovement);//
            bool isMoveableToLast = !isTargetNull && (areViaAndLastSame || movingPiece.TryToGetPieceMovement(startPosition + endPosition - viaPosition, out via2EndPieceMovement));//
            if (isTargetNull || (!areViaAndLastSame && isViaPieceNull) || !isOwner || isSameOwner || !isMoveableToVia || !isMoveableToLast)//
            {
                return VerifiedMove.InvalidMove;
            }

            //経路計算
            //worldPathに開始地点は含まれない
            var worldPath = WorldPathCalculator.CalculatePath(startPosition, viaPosition, endPosition, pieces, start2ViaPieceMovement, via2EndPieceMovement);
            var viaPositionNode = worldPath.First(value => value == viaPosition);  //経由点を通過するのが1回だけだということは保証されている.

            //経路の合法性チェック
            var surmountLimit = via2EndPieceMovement.Surmountable ? 1 : 0;
            var noPieceOnPath = worldPath.Where(node => node != viaPosition).Where(node => node != endPosition)
                .Select(node => pieces.Read(node)).Where(piece => piece != null)
                .Count() <= surmountLimit;
            if (!noPieceOnPath) return VerifiedMove.InvalidMove;

            return new VerifiedMove(movingPiece, player, worldPath, viaPositionNode);
        }

        bool IsOnBoard(IntegerVector2 position)
        {
            return position.x >= 0 && position.y >= 0 && position.x < pieces.Width && position.y < pieces.Height;
        }
    }
}