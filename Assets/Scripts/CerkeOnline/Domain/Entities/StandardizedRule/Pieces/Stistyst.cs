using System.Collections.Generic;
using UnityEngine;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.Pieces
{
    internal class Stistyst : DefaultPiece
    {
        protected readonly PieceMovement[] normalPieceMovements;
        protected readonly PieceMovement[] expansionPieceMovements;

        internal Stistyst(Terminologies.PieceColor color, Vector2Int position, IPlayer owner, IExpandingMoveFieldChecker fieldChecker) : base(position, color, owner, Terminologies.PieceName.Stistyst, fieldChecker)
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