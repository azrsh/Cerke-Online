using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.Hands.PieceStackProviders
{
    internal class TheCulture : DefaultPieceStacksProviderr
    {
        public TheCulture()
        {
            HandName = HandName.TheCulture;
            pieceStacks = new PieceStack[] { new PieceStack(PieceName.Kua, 1), new PieceStack(PieceName.Terlsk, 1), new PieceStack(PieceName.Varxle, 1) };
        }
    }
}