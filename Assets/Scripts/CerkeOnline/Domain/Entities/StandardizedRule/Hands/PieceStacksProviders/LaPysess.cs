using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.Hands.PieceStackProviders
{
    internal class LaPysess : DefaultPieceStacksProviderr
    {
        internal LaPysess()
        {
            HandName = HandName.TheAnimals;
            pieceStacks = new PieceStack[] { new PieceStack(PieceName.Stistyst, 1), new PieceStack(PieceName.Dodor, 1) };
        }
    }
}