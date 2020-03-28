using System.Collections.Generic;
using UnityEngine;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.Pieces
{
    internal class Ales : DefaultPiece
    {
        readonly PieceMovement[] normalPieceMovements;
        readonly PieceMovement[] expansionPieceMovements;

        internal Ales(Terminologies.PieceColor color, PublicDataType.IntegerVector2 position, IPlayer owner, IExpandingMoveFieldChecker fieldChecker) : base(position, color, owner, Terminologies.PieceName.Ales, fieldChecker)
        {
            normalPieceMovements = new PieceMovement[]
            {
                new PieceMovement(new PublicDataType.IntegerVector2(0, 1), 1), new PieceMovement(new PublicDataType.IntegerVector2(0, -1), 1),
                new PieceMovement(new PublicDataType.IntegerVector2(1, 0), 1), new PieceMovement(new PublicDataType.IntegerVector2(-1, 0), 1),
                new PieceMovement(new PublicDataType.IntegerVector2(1, 1), 1), new PieceMovement(new PublicDataType.IntegerVector2(1, -1), 1),
                new PieceMovement(new PublicDataType.IntegerVector2(-1, 1), 1), new PieceMovement(new PublicDataType.IntegerVector2(-1, -1), 1)
            };
            expansionPieceMovements = normalPieceMovements;
        }

        public override IReadOnlyList<PieceMovement> GetMoveablePosition(bool isExpanded)
        {
            if (!isExpanded)
                return normalPieceMovements;

            return expansionPieceMovements;
        }
    }
}