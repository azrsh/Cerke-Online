using System.Linq;
using System.Collections.Generic;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    public class MovePredictor
    {
        readonly IBoard board;

        public MovePredictor(IBoard board)
        {
            this.board = board;
        }

        public IEnumerable<PublicDataType.IntegerVector2> PredictMoveableColumns(PublicDataType.IntegerVector2 hypotheticalPosition, IReadOnlyPiece movingPiece)
        {
            if (movingPiece == null || !board.IsOnBoard(hypotheticalPosition)) 
                return Enumerable.Empty<PublicDataType.IntegerVector2>();

            bool isFrontPlayer = movingPiece.Owner != null && movingPiece.Owner.Encampment == Terminologies.Encampment.Front;
            PieceMovement unused;
            IEnumerable<PublicDataType.IntegerVector2> allColumn
                = Enumerable.Range(0, board.Width * board.Height)
                .Select(x => new PublicDataType.IntegerVector2(x / board.Height, x % board.Height));

            IEnumerable<PublicDataType.IntegerVector2> moveable = allColumn
                .Select(absolutePosition => (absolutePosition - hypotheticalPosition) * (isFrontPlayer ? -1 : 1))
                .Where(relativePosition => movingPiece.TryToGetPieceMovementByRelativePosition(relativePosition, out unused))
                .Select(relativePosition => relativePosition * (isFrontPlayer ? -1 : 1) + hypotheticalPosition);

            return moveable;
        }
    }
}