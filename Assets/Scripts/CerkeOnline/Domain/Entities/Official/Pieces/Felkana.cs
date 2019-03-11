using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Azarashi.CerkeOnline.Domain.Entities.Official.Pieces
{
    public class Felkana : DefaultPiece
    {
        protected readonly PieceMovement[] normalPieceMovements;
        protected readonly PieceMovement[] expansionPieceMovements;
        
        public Felkana(int color, Vector2Int position, IPlayer owner) : base(position, color, owner, Terminologies.PieceName.Felkana)
        {
            normalPieceMovements = new PieceMovement[]
            {
                new PieceMovement(new Vector2Int(0, 1), -1)
            };
            expansionPieceMovements = normalPieceMovements.Union(new PieceMovement[]
            {
                new PieceMovement(new Vector2Int(0,-1), -1), new PieceMovement(new Vector2Int(1,0), 2), new PieceMovement(new Vector2Int(-1,0), 2)
            }).ToArray();
        }
        
        public override IReadOnlyList<PieceMovement> GetMoveablePosition(bool isExpanded)
        {
            if(!isExpanded)
                return normalPieceMovements;

            return expansionPieceMovements;
        }
    }
}