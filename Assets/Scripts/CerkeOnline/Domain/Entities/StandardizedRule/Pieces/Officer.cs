using System.Linq;
using System.Collections.Generic;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.Pieces
{
    internal class Officer : DefaultPiece
    {
        protected readonly PieceMovement[] normalPieceMovements;
        protected readonly PieceMovement[] expansionPieceMovements;

        public Officer(Terminologies.PieceColor color, PublicDataType.IntegerVector2 position, IPlayer owner, IExpandingMoveFieldChecker fieldChecker)
            : base(position, color, owner, Terminologies.PieceName.Officer, fieldChecker)
        {
            normalPieceMovements = new PieceMovement[]
            {
                new PieceMovement(new PublicDataType.IntegerVector2(0, 1), -1), new PieceMovement(new PublicDataType.IntegerVector2(0, -1), -1),
                new PieceMovement(new PublicDataType.IntegerVector2(1, 0), 1), new PieceMovement(new PublicDataType.IntegerVector2(-1, 0), 1),
            };
            expansionPieceMovements = normalPieceMovements.Union(new PieceMovement[]
            {
                new PieceMovement(new PublicDataType.IntegerVector2(1, 0), -1), new PieceMovement(new PublicDataType.IntegerVector2(-1, 0), -1),
            }).ToArray();
        }

        public override IEnumerable<PieceMovement> GetMoveablePosition(bool isExpanded)
        {
            if (!isExpanded)
                return normalPieceMovements;

            return expansionPieceMovements;
        }
    }
}