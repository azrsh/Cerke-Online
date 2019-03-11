using System.Collections.Generic;
using UnityEngine;

namespace Azarashi.CerkeOnline.Domain.Entities.Official.Pieces
{
    public class Stistyst : DefaultPiece
    {
        protected readonly PieceMovement[] normalPieceMovements;
        protected readonly PieceMovement[] expansionPieceMovements;

        public Stistyst(int color, Vector2Int position, IPlayer owner) : base(position, color, owner, Terminologies.PieceName.Stistyst)
        {
            normalPieceMovements = new PieceMovement[]
            {
                new PieceMovement(new Vector2Int(1, 1), 1), new PieceMovement(new Vector2Int(1, -1), 1),
                new PieceMovement(new Vector2Int(-1, 1), 1), new PieceMovement(new Vector2Int(-1, -1), 1),
            };
            expansionPieceMovements = new PieceMovement[]
            {
                new PieceMovement(new Vector2Int(1, 1), -1), new PieceMovement(new Vector2Int(1, -1), -1),
                new PieceMovement(new Vector2Int(-1, 1), -1), new PieceMovement(new Vector2Int(-1, -1), -1),
            };
        }

        public override IReadOnlyList<PieceMovement> GetMoveablePosition(bool isExpanded)
        {
            if (!isExpanded)
                return normalPieceMovements;

            return expansionPieceMovements;
        }
    }
}