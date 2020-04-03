using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.Hands.PieceStackProviders
{
    internal class TheCavalry : DefaultPieceStacksProviderr
    {
        public TheCavalry()
        {
            HandName = HandName.TheCavalry;
            pieceStacks = new PieceStack[] { new PieceStack(PieceName.Elmer, 1), new PieceStack(PieceName.Gustuer, 1), new PieceStack(PieceName.Dodor, 1) };
        }
    }
}