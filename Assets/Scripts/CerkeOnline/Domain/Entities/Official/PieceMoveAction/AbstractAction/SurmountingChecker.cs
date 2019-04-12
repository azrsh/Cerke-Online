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
        readonly IValueInputProvider<int> valueProvider;
        readonly Action<IPiece, LinkedListNode<ColumnData>> onJudgementFailure;

        public SurmountingChecker(Mover pieceMover, WaterEntryChecker waterEntryChecker, IValueInputProvider<int> valueProvider, Action<IPiece, LinkedListNode<ColumnData>> onJudgementFailure)
        {
            this.pieceMover = pieceMover;
            this.waterEntryChecker = waterEntryChecker;
            this.valueProvider = valueProvider;
            this.onJudgementFailure = onJudgementFailure;
        }

        void CheckSurmounting(PieceMovement viaPieceMovement, IPiece movingPiece, LinkedListNode<ColumnData> worldPathNode, bool surmounted, Action surmountAction)
        {
            var nextPiece = worldPathNode.Value.Piece;
            var isLast = worldPathNode.Next == null;
            var isSurmountable = nextPiece != null && !surmounted && viaPieceMovement.surmountable && !isLast;
            if (isSurmountable)
            {
                //別の書き方にしたい
                if (waterEntryChecker.IsJudgmentNecessary(movingPiece, worldPathNode) ||
                    waterEntryChecker.IsJudgmentNecessary(movingPiece, worldPathNode.Next))
                {
                    if (worldPathNode.Previous != null) pieceMover.MovePiece(movingPiece, worldPathNode.Previous.Value.Positin);
                    valueProvider.RequestValue(value =>
                    {
                        if (value < 3)
                        {
                            onJudgementFailure(movingPiece, worldPathNode);
                            return;
                        }

                        surmountAction();
                    });
                }
                else
                    surmountAction();

                return;
            }
        }
    }
}