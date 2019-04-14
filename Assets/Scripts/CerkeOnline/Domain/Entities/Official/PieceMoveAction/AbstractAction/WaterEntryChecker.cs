using System;
using System.Collections.Generic;
using UnityEngine;
using Azarashi.CerkeOnline.Domain.Entities.Official.PieceMoveAction.DataStructure;

namespace Azarashi.CerkeOnline.Domain.Entities.Official.PieceMoveAction.AbstractAction
{
    public class WaterEntryChecker
    {
        readonly int threshold;
        readonly IFieldEffectChecker fieldEffectChecker;
        readonly IValueInputProvider<int> valueProvider;
        readonly Action<IPiece, LinkedListNode<ColumnData>> onJudgementFailure;

        public WaterEntryChecker(int threshold, IFieldEffectChecker fieldEffectChecker,IValueInputProvider<int> valueProvider, Action<IPiece, LinkedListNode<ColumnData>> onJudgementFailure)
        {
            this.threshold = threshold;
            this.fieldEffectChecker = fieldEffectChecker;
            this.valueProvider = valueProvider;
            this.onJudgementFailure = onJudgementFailure;
        }

        public bool CheckWaterEntry(IPiece movingPiece, LinkedListNode<ColumnData> worldPathNode, Action onSuccess)
        {
            if (!IsJudgmentNecessary(movingPiece, worldPathNode)) return true;

            JudgeWaterEntry(movingPiece, worldPathNode, onSuccess);
            return false;
        }

        public bool IsJudgmentNecessary(IPiece movingPiece, LinkedListNode<ColumnData> worldPathNode)
        {
            Vector2Int start = movingPiece.Position;
            bool isInWater = (worldPathNode.Previous != null && fieldEffectChecker.IsInTammua(worldPathNode.Previous.Value.Positin)) || (worldPathNode.Previous == null && fieldEffectChecker.IsInTammua(start));
            bool isIntoWater = fieldEffectChecker.IsInTammua(worldPathNode.Value.Positin);
            bool canLittuaWithoutJudge = movingPiece.CanLittuaWithoutJudge();
            bool isNecessaryWaterEntryJudgment = !isInWater && isIntoWater && !canLittuaWithoutJudge;
            return isNecessaryWaterEntryJudgment;
        }

        public void JudgeWaterEntry(IPiece movingPiece, LinkedListNode<ColumnData> worldPathNode, Action onSuccess)
        {
            valueProvider.RequestValue(value =>
            {
                if (value < threshold)
                {
                    onJudgementFailure?.Invoke(movingPiece, worldPathNode);
                    return;
                }

                onSuccess?.Invoke();
            });
        }
    }
}