using System.Collections.Generic;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.Pieces
{
    internal class Tiger : DefaultPiece
    {
        protected readonly PieceMovement[] normalPieceMovements;
        protected readonly PieceMovement[] expansionPieceMovements;

        public Tiger(Terminologies.PieceColor color, PublicDataType.IntegerVector2 position, IPlayer owner, IExpandingMoveFieldChecker fieldChecker)
            : base(position, color, owner, Terminologies.PieceName.Tiger, fieldChecker)
        {
            normalPieceMovements = new PieceMovement[]
            {
                new PieceMovement(new PublicDataType.IntegerVector2(1, 1), 1), new PieceMovement(new PublicDataType.IntegerVector2(1, -1), 1),
                new PieceMovement(new PublicDataType.IntegerVector2(-1, 1), 1), new PieceMovement(new PublicDataType.IntegerVector2(-1, -1), 1),
            };
            expansionPieceMovements = new PieceMovement[]
            {
                new PieceMovement(new PublicDataType.IntegerVector2(1, 1), -1), new PieceMovement(new PublicDataType.IntegerVector2(1, -1), -1),
                new PieceMovement(new PublicDataType.IntegerVector2(-1, 1), -1), new PieceMovement(new PublicDataType.IntegerVector2(-1, -1), -1),
            };
        }

        public override IEnumerable<PieceMovement> GetMoveablePosition(bool isExpanded)
        {
            if (!isExpanded)
                return normalPieceMovements;

            return expansionPieceMovements;
        }
    }
}