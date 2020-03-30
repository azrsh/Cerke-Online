﻿using System.Linq;
using System.Collections.Generic;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.Pieces
{
    internal class Dodor : DefaultPiece
    {
        protected readonly PieceMovement[] normalPieceMovements;
        protected readonly PieceMovement[] expansionPieceMovements;

        public Dodor(Terminologies.PieceColor color, PublicDataType.IntegerVector2 position, IPlayer owner, IExpandingMoveFieldChecker fieldChecker) : base(position, color, owner, Terminologies.PieceName.Dodor, fieldChecker)
        {
            normalPieceMovements = new PieceMovement[]
            {
                new PieceMovement(new PublicDataType.IntegerVector2(2, 2), 1), new PieceMovement(new PublicDataType.IntegerVector2(2, -2), 1),
                new PieceMovement(new PublicDataType.IntegerVector2(-2, 2), 1), new PieceMovement(new PublicDataType.IntegerVector2(-2, -2), 1),
            };
            expansionPieceMovements = new PieceMovement[]
            {
                new PieceMovement(new PublicDataType.IntegerVector2(2, 2), -1), new PieceMovement(new PublicDataType.IntegerVector2(2, -2), -1),
                new PieceMovement(new PublicDataType.IntegerVector2(-2, 2), -1), new PieceMovement(new PublicDataType.IntegerVector2(-2, -2), -1),
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