using System.Collections.Generic;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    internal abstract class DefaultPieceStacksProviderr : IPieceStacksProvider
    {
        protected PieceStack[] pieceStacks;

        public IEnumerable<PieceStack> GetPieceStacks() => pieceStacks;
    }
}