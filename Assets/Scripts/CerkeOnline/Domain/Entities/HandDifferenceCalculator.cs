using System.Collections.Generic;
using System.Linq;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    public static class HandDifferenceCalculator
    {
        public static HandDifference Calculate(IEnumerable<IHand> previous, IEnumerable<IHand> next)
        {
            var increasedDifference = next.Except(previous);
            var decreasedDifference = previous.Except(next);
            return new HandDifference(increasedDifference, decreasedDifference);
        }
    }

    public readonly struct HandDifference
    {
        public IEnumerable<IHand> IncreasedDifference { get; }
        public IEnumerable<IHand> DecreasedDifference { get; }

        public HandDifference(IEnumerable<IHand> increasedDifference, IEnumerable<IHand> decreasedDifference)
        {
            IncreasedDifference = increasedDifference;
            DecreasedDifference = decreasedDifference;
        }
    }
}