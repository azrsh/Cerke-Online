using System.Collections.Generic;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    internal interface IPieceStacksProvider
    {
        IEnumerable<PieceStack> GetPieceStacks();
    }
}