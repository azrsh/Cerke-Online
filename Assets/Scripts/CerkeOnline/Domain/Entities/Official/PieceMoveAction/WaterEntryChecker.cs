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

        public WaterEntryChecker(int threshold, IFieldEffectChecker fieldEffectChecker,IValueInputProvider<int> valueProvider)
        {
            this.threshold = threshold;
            this.fieldEffectChecker = fieldEffectChecker;
            this.valueProvider = valueProvider;
        }

        public void CheckWaterEntry(IPiece movingPiece, LinkedListNode<ColumnData> worldPathNode, Action onSuccess, Action onFailure)
        {
            if (IsJudgmentNecessary(movingPiece, worldPathNode))
            {
                JudgeWaterEntry(onSuccess, onFailure);
                return;
            }

            onSuccess();
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

        void JudgeWaterEntry(Action onSuccess, Action onFailure)
        {
            valueProvider.RequestValue(value =>
            {
                if (value < threshold)
                {
                    onFailure();
                    return;
                }

                onSuccess();
            });
        }
    }
}