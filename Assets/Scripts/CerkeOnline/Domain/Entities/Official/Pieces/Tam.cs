using System.Collections.Generic;
using UnityEngine;

namespace Azarashi.CerkeOnline.Domain.Entities.Official.Pieces
{
    public class Tam : DefaultPiece
    {
        readonly PieceMovement[] normalPieceMovements;
        readonly PieceMovement[] expansionPieceMovements;

        public Tam(int color, Vector2Int position, IPlayer owner) : base(position, color, owner, Terminologies.PieceName.Tam)
        {
            normalPieceMovements = new PieceMovement[]
            {
                new PieceMovement(new Vector2Int(0, 1), 1, false, 2), new PieceMovement(new Vector2Int(0, -1), 1, false, 2),
                new PieceMovement(new Vector2Int(1, 0), 1, false, 2), new PieceMovement(new Vector2Int(-1, 0), 1, false, 2),
                new PieceMovement(new Vector2Int(1, 1), 1, false, 2), new PieceMovement(new Vector2Int(1, -1), 1, false, 2),
                new PieceMovement(new Vector2Int(-1, 1), 1, false, 2), new PieceMovement(new Vector2Int(-1, -1), 1, false, 2)
            };
            expansionPieceMovements = normalPieceMovements;
        }

        public override IReadOnlyList<PieceMovement> GetMoveablePosition(bool isExpanded)
        {
            if (!isExpanded)
                return normalPieceMovements;

            return expansionPieceMovements;
        }
    }
}