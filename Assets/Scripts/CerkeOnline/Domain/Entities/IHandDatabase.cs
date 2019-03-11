using System.Collections.Generic;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    public interface IHandDatabase
    {
        IHand[] SearchHands(IReadOnlyList<IReadOnlyPiece> pieces);
    }
}