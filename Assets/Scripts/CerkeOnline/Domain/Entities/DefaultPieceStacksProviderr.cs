using System.Collections.Generic;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    internal abstract class DefaultPieceStacksProviderr : IPieceStacksProvider
    {
        public Terminologies.HandName HandName { get; protected set; }

        protected PieceStack[] pieceStacks;

        public IEnumerable<PieceStack> GetPieceStacks() => pieceStacks;
    }
}