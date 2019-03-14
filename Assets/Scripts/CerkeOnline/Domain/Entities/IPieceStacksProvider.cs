using System.Collections.Generic;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    public interface IPieceStacksProvider
    {
        IReadOnlyList<PieceStack> GetPieceStacks();
    }
}