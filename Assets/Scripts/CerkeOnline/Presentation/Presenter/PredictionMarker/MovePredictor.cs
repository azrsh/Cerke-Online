using System.Collections.Generic;
using UnityEngine;
using Azarashi.CerkeOnline.Domain.Entities;

public class MovePredictor
{
    readonly IBoard board;

    public MovePredictor(IBoard board)
    {
        this.board = board;
    }

    public IReadOnlyList<Vector2Int> PredictMoveableColumns(IReadOnlyPiece movingPiece)
    {
        List<Vector2Int> result = new List<Vector2Int>();

        if (movingPiece == null) return result;

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                Vector2Int currentPosition = new Vector2Int(i, j);
                if (board.IsOnBoard(currentPosition) && movingPiece.IsMoveable(currentPosition))
                    result.Add(currentPosition);
            }
        }

        return result;
    }
}
