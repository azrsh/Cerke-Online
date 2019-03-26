using System.Collections.Generic;
using UnityEngine;

namespace Azarashi.CerkeOnline.Domain.Entities.Official.Pieces
{
    public class Terlsk : DefaultPiece
    {
        readonly PieceMovement[] normalPieceMovements;
        readonly PieceMovement[] expansionPieceMovements;

        public Terlsk(int color, Vector2Int position, IPlayer owner, IExpandingMoveFieldChecker fieldChecker) : base(position, color, owner, Terminologies.PieceName.Terlsk, fieldChecker)
        {
            normalPieceMovements = new PieceMovement[]
            {
                new PieceMovement(new Vector2Int(0, 1), 1), new PieceMovement(new Vector2Int(0, -1), 1),
                new PieceMovement(new Vector2Int(1, 0), 1), new PieceMovement(new Vector2Int(-1, 0), 1)
            };
            expansionPieceMovements = new PieceMovement[]
            {
                new PieceMovement(new Vector2Int(0, 1), -1, true), new PieceMovement(new Vector2Int(0, -1), -1, true),
                new PieceMovement(new Vector2Int(1, 0), -1, true), new PieceMovement(new Vector2Int(-1, 0), -1, true),
                new PieceMovement(new Vector2Int(1, 1), -1, true), new PieceMovement(new Vector2Int(1, -1), -1, true),
                new PieceMovement(new Vector2Int(-1, 1), -1, true), new PieceMovement(new Vector2Int(-1, -1), -1, true)
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