using System.Linq;
using System.Collections.Generic;

namespace Azarashi.CerkeOnline.Domain.Entities.NoneRule
{
    public class HandDatabase : IHandDatabase
    {
        readonly IHand[] hands = { };

        public IHand[] SearchHands(IReadOnlyList<IReadOnlyPiece> pieces)
        {
            return hands.Where(hand => hand.IsSccess(pieces)).ToArray();
        }
    }
}