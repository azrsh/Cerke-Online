﻿using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Azarashi.CerkeOnline.Domain.Entities.Official.Pieces
{
    public class Dodor : DefaultPiece
    {
        protected readonly PieceMovement[] normalPieceMovements;
        protected readonly PieceMovement[] expansionPieceMovements;

        public Dodor(Terminologies.PieceColor color, Vector2Int position, IPlayer owner, IExpandingMoveFieldChecker fieldChecker) : base(position, color, owner, Terminologies.PieceName.Dodor, fieldChecker)
        {
            normalPieceMovements = new PieceMovement[]
            {
                new PieceMovement(new Vector2Int(2, 2), 1), new PieceMovement(new Vector2Int(2, -2), 1),
                new PieceMovement(new Vector2Int(-2, 2), 1), new PieceMovement(new Vector2Int(-2, -2), 1),
            };
            expansionPieceMovements = new PieceMovement[]
            {
                new PieceMovement(new Vector2Int(2, 2), -1), new PieceMovement(new Vector2Int(2, -2), -1),
                new PieceMovement(new Vector2Int(-2, 2), -1), new PieceMovement(new Vector2Int(-2, -2), -1),
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