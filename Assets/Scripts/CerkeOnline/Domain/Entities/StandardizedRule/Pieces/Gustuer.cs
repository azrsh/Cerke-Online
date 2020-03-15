using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.Pieces
{
    public class Gustuer : DefaultPiece
    {
        protected readonly PieceMovement[] normalPieceMovements;
        protected readonly PieceMovement[] expansionPieceMovements;

        public Gustuer(Terminologies.PieceColor color, Vector2Int position, IPlayer owner, IExpandingMoveFieldChecker fieldChecker) : base(position, color, owner, Terminologies.PieceName.Gustuer, fieldChecker)
        {
            normalPieceMovements = new PieceMovement[]
            {
                new PieceMovement(new Vector2Int(0, 1), -1), new PieceMovement(new Vector2Int(0, -1), -1),
                new PieceMovement(new Vector2Int(1, 0), -1), new PieceMovement(new Vector2Int(-1, 0), -1),
            };
            expansionPieceMovements = new PieceMovement[]
            {
                new PieceMovement(new Vector2Int(1,1), -1), new PieceMovement(new Vector2Int(1,-1), -1),
                new PieceMovement(new Vector2Int(-1,1), -1), new PieceMovement(new Vector2Int(-1,-1), -1)
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