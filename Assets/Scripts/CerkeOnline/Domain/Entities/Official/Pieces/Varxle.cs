using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Azarashi.CerkeOnline.Domain.Entities.Official.Pieces
{
    public class Varxle : DefaultPiece
    {
        readonly PieceMovement[] normalPieceMovements;
        readonly PieceMovement[] expansionPieceMovements;

        public Varxle(int color, Vector2Int position, IPlayer owner) : base(position, color, owner, Terminologies.PieceName.Varxle)
        {
            normalPieceMovements = new PieceMovement[]
            {
                new PieceMovement(new Vector2Int(0, 1), 1),
                new PieceMovement(new Vector2Int(1, 0), 1), new PieceMovement(new Vector2Int(-1, 0), 1),
                new PieceMovement(new Vector2Int(1, 1), 1), new PieceMovement(new Vector2Int(1, -1), 1),
                new PieceMovement(new Vector2Int(-1, 1), 1), new PieceMovement(new Vector2Int(-1, -1), 1)
            };
            expansionPieceMovements = normalPieceMovements.Union(new PieceMovement[]
            {
                 new PieceMovement(new Vector2Int(0, -1), 1)
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