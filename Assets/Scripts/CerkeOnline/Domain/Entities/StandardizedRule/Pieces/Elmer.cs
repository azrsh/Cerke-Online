using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.Pieces
{
    internal class Elmer : DefaultPiece
    {
        protected readonly PieceMovement[] normalPieceMovements;
        protected readonly PieceMovement[] expansionPieceMovements;

        internal Elmer(Terminologies.PieceColor color, PublicDataType.IntegerVector2 position, IPlayer owner, IExpandingMoveFieldChecker fieldChecker) : base(position, color, owner, Terminologies.PieceName.Elmer, fieldChecker)
        {
            normalPieceMovements = new PieceMovement[]
            {
                new PieceMovement(new PublicDataType.IntegerVector2(0, 1), 1)
            };
            expansionPieceMovements = normalPieceMovements.Union(new PieceMovement[]
            {
                new PieceMovement(new PublicDataType.IntegerVector2(0,1), 2), new PieceMovement(new PublicDataType.IntegerVector2(0,-1), 1),
                new PieceMovement(new PublicDataType.IntegerVector2(1,0), 1), new PieceMovement(new PublicDataType.IntegerVector2(-1,0), 1)
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