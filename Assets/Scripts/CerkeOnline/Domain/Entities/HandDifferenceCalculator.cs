using System.Linq;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    public static class HandDifferenceCalculator
    {
        public static HandDifference Calculate(IHand[] previous, IHand[] next)
        {
            var increasedDifference = next?.Except(previous ?? new IHand[] { })?.ToArray() ?? next;
            var decreasedDifference = previous?.Except(next)?.ToArray() ?? new IHand[] { };
            return new HandDifference(increasedDifference, decreasedDifference);
        }
    }

    public struct HandDifference
    {
        public IHand[] IncreasedDifference { get; }
        public IHand[] DecreasedDifference { get; }

        public HandDifference(IHand[] increasedDifference, IHand[] decreasedDifference)
        {
            IncreasedDifference = increasedDifference;
            DecreasedDifference = decreasedDifference;
        }
    }
}