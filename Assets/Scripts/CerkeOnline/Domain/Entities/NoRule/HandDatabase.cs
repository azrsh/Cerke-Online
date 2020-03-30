using System.Linq;
using System.Collections.Generic;

namespace Azarashi.CerkeOnline.Domain.Entities.NoRule
{
    internal class HandDatabase : IHandDatabase
    {
        readonly IEnumerable<IHand> hands = Enumerable.Empty<IHand>();

        public IEnumerable<IHand> SearchHands(IEnumerable<IReadOnlyPiece> pieces)
        {
            return  hands;
        }
    }
}