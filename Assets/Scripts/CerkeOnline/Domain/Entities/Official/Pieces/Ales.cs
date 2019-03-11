using System.Collections.Generic;
using UnityEngine;

namespace Azarashi.CerkeOnline.Domain.Entities.Official.Pieces
{
    public class Ales : DefaultPiece
    {
        readonly PieceMovement[] normalPieceMovements;
        readonly PieceMovement[] expansionPieceMovements;

        public Ales(int color, Vector2Int position, IPlayer owner) : base(position, color, owner, Terminologies.PieceName.Ales)
        {
            normalPieceMovements = new PieceMovement[]
            {
                new PieceMovement(new Vector2Int(0, 1), 1), new PieceMovement(new Vector2Int(0, -1), 1),
                new PieceMovement(new Vector2Int(1, 0), 1), new PieceMovement(new Vector2Int(-1, 0), 1),
                new PieceMovement(new Vector2Int(1, 1), 1), new PieceMovement(new Vector2Int(1, -1), 1),
                new PieceMovement(new Vector2Int(-1, 1), 1), new PieceMovement(new Vector2Int(-1, -1), 1)
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