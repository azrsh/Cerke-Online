using System;
using System.Collections.Generic;
using Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.PieceMoveAction.ActualAction;
using Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.PieceMoveAction.DataStructure;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.PieceMoveAction.AbstractAction
{
    internal class SurmountingChecker
    {
        readonly Mover pieceMover;

        public SurmountingChecker(Mover pieceMover)
        {
            this.pieceMover = pieceMover;
        }

        public bool CheckSurmounting(PieceMovement viaPieceMovement, IPiece movingPiece, LinkedListNode<ColumnData> worldPathNode, bool surmounted, Action surmountAction)
        {
            var nextPiece = worldPathNode.Value.Piece;
            var isLast = worldPathNode.Next == null;
            var isSurmountable = nextPiece != null && !surmounted && viaPieceMovement.surmountable && !isLast;
            if (!isSurmountable)
                return true;

            surmountAction();

            return false;
        }
    }
}