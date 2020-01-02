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

        public IReadOnlyList<Vector2Int> PredictMoveableColumns(Vector2Int hypotheticalPosition, IReadOnlyPiece movingPiece)
        {
            List<Vector2Int> result = new List<Vector2Int>();

            if (movingPiece == null || !board.IsOnBoard(hypotheticalPosition)) return result;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Vector2Int currentPosition = new Vector2Int(i, j);
                    bool isFrontPlayer = movingPiece.Owner != null && movingPiece.Owner.Encampment == Terminologies.Encampment.Front;
                    Vector2Int relativePosition = (currentPosition - hypotheticalPosition) * (isFrontPlayer ? -1 : 1);
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