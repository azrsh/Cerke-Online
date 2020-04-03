using System.Collections.Generic;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.Pieces
{
    internal class Shaman : DefaultPiece
    {
        readonly PieceMovement[] normalPieceMovements;
        readonly PieceMovement[] expansionPieceMovements;

        public Shaman(Terminologies.PieceColor color, PublicDataType.IntegerVector2 position, IPlayer owner, IExpandingMoveFieldChecker fieldChecker)
            : base(position, color, owner, Terminologies.PieceName.Shaman, fieldChecker)
        {
            normalPieceMovements = new PieceMovement[]
            {
                new PieceMovement(new PublicDataType.IntegerVector2(0, 1), 1), new PieceMovement(new PublicDataType.IntegerVector2(0, -1), 1),
                new PieceMovement(new PublicDataType.IntegerVector2(1, 0), -1), new PieceMovement(new PublicDataType.IntegerVector2(-1, 0), -1)
            };
            expansionPieceMovements = new PieceMovement[]
            {
                new PieceMovement(new PublicDataType.IntegerVector2(0, 1), -1, true), new PieceMovement(new PublicDataType.IntegerVector2(0, -1), -1, true),
                new PieceMovement(new PublicDataType.IntegerVector2(1, 0), -1, true), new PieceMovement(new PublicDataType.IntegerVector2(-1, 0), -1, true),
                new PieceMovement(new PublicDataType.IntegerVector2(1, 1), -1, true), new PieceMovement(new PublicDataType.IntegerVector2(1, -1), -1, true),
                new PieceMovement(new PublicDataType.IntegerVector2(-1, 1), -1, true), new PieceMovement(new PublicDataType.IntegerVector2(-1, -1), -1, true)
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