using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Azarashi.CerkeOnline.Domain.Entities.Official.Pieces
{
    public class Felkana : DefaultPiece
    {
        protected readonly PieceMovement[] normalPieceMovements;
        protected readonly PieceMovement[] expansionPieceMovements;
        
        public Felkana(Terminologies.PieceColor color, Vector2Int position, IPlayer owner, IExpandingMoveFieldChecker fieldChecker) : base(position, color, owner, Terminologies.PieceName.Felkana, fieldChecker)
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
        
        public override bool CanLittuaWithoutJudge() => true;
    }
}