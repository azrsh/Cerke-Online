using System;
using System.Linq;
using System.Collections.Generic;
using UniRx;
using Azarashi.CerkeOnline.Domain.Entities.Official.Hands;
using Azarashi.CerkeOnline.Domain.Entities.Official.Hands.PieceStackProviders;

namespace Azarashi.CerkeOnline.Domain.Entities.Official
{
    public class OverwriteHandPair
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