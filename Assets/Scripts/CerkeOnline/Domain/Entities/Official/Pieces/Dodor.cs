using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Azarashi.CerkeOnline.Domain.Entities.Official.Pieces
{
    public class Dodor : DefaultPiece
    {
        protected readonly PieceMovement[] normalPieceMovements;
        protected readonly PieceMovement[] expansionPieceMovements;

        public Dodor(int color, Vector2Int position, IPlayer owner, IExpandingMoveFieldChecker fieldChecker) : base(position, color, owner, Terminologies.PieceName.Dodor, fieldChecker)
        {
            normalPieceMovements = new PieceMovement[]
            {
                new PieceMovement(new Vector2Int(1, 1), 2), new PieceMovement(new Vector2Int(1, -1), 2),
                new PieceMovement(new Vector2Int(-1, 1), 2), new PieceMovement(new Vector2Int(-1, -1), 2),
            };
            expansionPieceMovements = normalPieceMovements.Union(new PieceMovement[]
            {
                new PieceMovement(new Vector2Int(0, 1), 2), new PieceMovement(new Vector2Int(0, -1), 2),
                new PieceMovement(new Vector2Int(1, 0), 2), new PieceMovement(new Vector2Int(-1, 0), 2),
            }).ToArray();
        }

        public override IReadOnlyList<PieceMovement> GetMoveablePosition(bool isExpanded)
        {
            if (!isExpanded)
                return normalPieceMovements;

            return expansionPieceMovements;
        }
    }
}