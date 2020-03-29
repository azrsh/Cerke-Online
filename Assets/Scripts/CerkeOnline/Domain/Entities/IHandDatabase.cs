using System.Collections.Generic;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    public interface IHandDatabase
    {
        IEnumerable<IHand> SearchHands(IEnumerable<IReadOnlyPiece> pieces);
    }
}