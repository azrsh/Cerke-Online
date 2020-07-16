using System;
using UniRx.Async;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.PieceMoveAction.AbstractAction
{
    internal class WaterEntryChecker
    {
        readonly int threshold;
        readonly IFieldEffectChecker fieldEffectChecker;
        readonly IValueInputProvider<int> valueProvider;

        public WaterEntryChecker(int threshold, IFieldEffectChecker fieldEffectChecker, 
            IValueInputProvider<int> valueProvider)
        {
            this.threshold = threshold;
            this.fieldEffectChecker = fieldEffectChecker;
            this.valueProvider = valueProvider;
        }

        public async UniTask<bool> CheckWaterEntry(IPiece movingPiece, PublicDataType.IntegerVector2 start, PublicDataType.IntegerVector2 end)
        {
            return !IsJudgmentNecessary(movingPiece, start, end) ||  await JudgeWaterEntry();
        }

        bool IsJudgmentNecessary(IPiece movingPiece,PublicDataType.IntegerVector2 start, PublicDataType.IntegerVector2 end)
        {
            bool isInWater = fieldEffectChecker.IsInTammua(start);
            bool isIntoWater = fieldEffectChecker.IsInTammua(end);
            bool canLittuaWithoutJudge = movingPiece.CanLittuaWithoutJudge();
            bool isNecessaryWaterEntryJudgment = !isInWater && isIntoWater && !canLittuaWithoutJudge;
            return isNecessaryWaterEntryJudgment;
        }

        UniTask<bool> JudgeWaterEntry() => valueProvider.RequestValue().ContinueWith(value => value >= threshold);
    }
}