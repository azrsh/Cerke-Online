using System.Collections.Generic;

namespace Azarashi.CerkeOnline.Domain.Entities.NoRule
{
    internal class HandDatabase : IHandDatabase
    {
        readonly IHand[] hands = { };

        public IHand[] SearchHands(IReadOnlyList<IReadOnlyPiece> pieces)
        {
            return  hands;
        }
    }
}