using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.Pieces
{
    internal class Vadyrd : DefaultPiece
    {
        protected readonly PieceMovement[] normalPieceMovements;
        protected readonly PieceMovement[] expansionPieceMovements;

        internal Vadyrd(Terminologies.PieceColor color, PublicDataType.IntegerVector2 position, IPlayer owner, IExpandingMoveFieldChecker fieldChecker) : base(position, color, owner, Terminologies.PieceName.Vadyrd, fieldChecker)
        {
            normalPieceMovements = new PieceMovement[]
            {
                new PieceMovement(new PublicDataType.IntegerVector2(2, 0), 1), new PieceMovement(new PublicDataType.IntegerVector2(-2, 0), 1),
                new PieceMovement(new PublicDataType.IntegerVector2(0, 2), 1), new PieceMovement(new PublicDataType.IntegerVector2(0, -2), -1),
            };
            expansionPieceMovements = new PieceMovement[]
            {
                new PieceMovement(new PublicDataType.IntegerVector2(2,2), 1), new PieceMovement(new PublicDataType.IntegerVector2(-2,2), 1),
                new PieceMovement(new PublicDataType.IntegerVector2(2,-2), 1), new PieceMovement(new PublicDataType.IntegerVector2(-2,-2), 1)
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