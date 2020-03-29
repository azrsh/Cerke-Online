using System.Collections.Generic;
using UnityEngine;

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
            List<PublicDataType.IntegerVector2> result = new List<PublicDataType.IntegerVector2>();

            if (movingPiece == null || !board.IsOnBoard(hypotheticalPosition)) return result;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    PublicDataType.IntegerVector2 currentPosition = new PublicDataType.IntegerVector2(i, j);
                    bool isFrontPlayer = movingPiece.Owner != null && movingPiece.Owner.Encampment == Terminologies.Encampment.Front;
                    PublicDataType.IntegerVector2 relativePosition = (currentPosition - hypotheticalPosition) * (isFrontPlayer ? -1 : 1);
                    PieceMovement unused;
                    if (board.IsOnBoard(currentPosition) &&
                        movingPiece.TryToGetPieceMovementByRelativePosition(relativePosition, out unused))
                        result.Add(currentPosition);
                }
            }

            return result;
        }
    }
}