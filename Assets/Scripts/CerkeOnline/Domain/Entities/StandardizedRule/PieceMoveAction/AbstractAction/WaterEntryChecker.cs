using System;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.PieceMoveAction.AbstractAction
{
    internal class WaterEntryChecker
    {
        readonly int threshold;
        readonly IFieldEffectChecker fieldEffectChecker;
        readonly IValueInputProvider<int> valueProvider;
        readonly Action<IPiece> onJudgementFailure;

        public WaterEntryChecker(int threshold, IFieldEffectChecker fieldEffectChecker, 
            IValueInputProvider<int> valueProvider, Action<IPiece> onJudgementFailure)
        {
            this.threshold = threshold;
            this.fieldEffectChecker = fieldEffectChecker;
            this.valueProvider = valueProvider;
            this.onJudgementFailure = onJudgementFailure;
        }

        public bool CheckWaterEntry(IPiece movingPiece, PublicDataType.IntegerVector2 start, PublicDataType.IntegerVector2 end, Action onSuccess)
        {
            if (!IsJudgmentNecessary(movingPiece, start,end)) return true;

            JudgeWaterEntry(movingPiece, onSuccess);
            return false;
        }

        public bool IsJudgmentNecessary(IPiece movingPiece,PublicDataType.IntegerVector2 start, PublicDataType.IntegerVector2 end)
        {
            bool isInWater = fieldEffectChecker.IsInTammua(start);
            bool isIntoWater = fieldEffectChecker.IsInTammua(end);
            bool canLittuaWithoutJudge = movingPiece.CanLittuaWithoutJudge();
            bool isNecessaryWaterEntryJudgment = !isInWater && isIntoWater && !canLittuaWithoutJudge;
            return isNecessaryWaterEntryJudgment;
        }

        public void JudgeWaterEntry(IPiece movingPiece, Action onSuccess)
        {
            valueProvider.RequestValue(value =>
            {
                if (value < threshold)
                {
                    onJudgementFailure?.Invoke(movingPiece);
                    return;
                }

                onSuccess?.Invoke();
            });
        }
    }
}