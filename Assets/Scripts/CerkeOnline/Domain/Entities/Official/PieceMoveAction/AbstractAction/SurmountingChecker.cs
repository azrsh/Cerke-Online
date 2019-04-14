using System;
using System.Collections.Generic;
using Azarashi.CerkeOnline.Domain.Entities.Official.PieceMoveAction.ActualAction;
using Azarashi.CerkeOnline.Domain.Entities.Official.PieceMoveAction.DataStructure;

namespace Azarashi.CerkeOnline.Domain.Entities.Official.PieceMoveAction.AbstractAction
{
    public class SurmountingChecker
    {
        readonly Mover pieceMover;
        readonly WaterEntryChecker waterEntryChecker;

        public SurmountingChecker(Mover pieceMover, WaterEntryChecker waterEntryChecker)
        {
            this.pieceMover = pieceMover;
            this.waterEntryChecker = waterEntryChecker;
        }

        public bool CheckSurmounting(PieceMovement viaPieceMovement, IPiece movingPiece, LinkedListNode<ColumnData> worldPathNode, bool surmounted, Action surmountAction)
        {
            var nextPiece = worldPathNode.Value.Piece;
            var isLast = worldPathNode.Next == null;
            var isSurmountable = nextPiece != null && !surmounted && viaPieceMovement.surmountable && !isLast;
            if (!isSurmountable)
                return true;

            if (waterEntryChecker.IsJudgmentNecessary(movingPiece, worldPathNode) ||
                waterEntryChecker.IsJudgmentNecessary(movingPiece, worldPathNode.Next))
            {
                if (worldPathNode.Previous != null) pieceMover.MovePiece(movingPiece, worldPathNode.Previous.Value.Positin);
                waterEntryChecker.JudgeWaterEntry(movingPiece, worldPathNode, surmountAction);
            }
            else
            {
                surmountAction();
            }

            return false;
        }
    }
}