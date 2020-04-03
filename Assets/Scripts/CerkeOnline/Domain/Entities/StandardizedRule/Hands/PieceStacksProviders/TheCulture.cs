using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.Hands.PieceStackProviders
{
    internal class TheCulture : DefaultPieceStacksProviderr
    {
        public TheCulture()
        {
            HandName = HandName.TheCulture;
            pieceStacks = new PieceStack[] { new PieceStack(PieceName.Officer, 1), new PieceStack(PieceName.Shaman, 1), new PieceStack(PieceName.General, 1) };
        }
    }
}