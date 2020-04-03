using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.Hands.PieceStackProviders
{
    internal class TheAnimals : DefaultPieceStacksProviderr
    {
        public TheAnimals()
        {
            HandName = HandName.TheAnimals;
            pieceStacks = new PieceStack[] { new PieceStack(PieceName.Stistyst, 1), new PieceStack(PieceName.Dodor, 1) };
        }
    }
}