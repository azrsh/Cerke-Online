using System.Linq;
using System.Collections.Generic;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule
{
    internal class OverwriteHandPair
    {
        public IHand Overwritten { get; }
        public IHand Overwriter { get; }

        public OverwriteHandPair(IHand overwritten, IHand overwriter)
        {
            Overwritten = overwritten;
            Overwriter = overwriter;
        }

        public bool IsOverwritable(IEnumerable<IHand> hands)
        {
            return hands.Contains(Overwriter) && hands.Contains(Overwritten);
        }
    }
}