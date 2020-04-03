using System.Collections.Generic;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    internal interface IPieceStacksProvider
    {
        Terminologies.HandName HandName { get; }
        IEnumerable<PieceStack> GetPieceStacks();
    }
}