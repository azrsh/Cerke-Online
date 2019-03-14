using System.Collections.Generic;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    public abstract class DefaultPieceStacksProviderr : IPieceStacksProvider
    {
        protected PieceStack[] pieceStacks;

        public IReadOnlyList<PieceStack> GetPieceStacks()
        {
            return pieceStacks;
        }
    }
}