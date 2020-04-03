using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.Hands.PieceStackProviders
{
    internal class TheCavalry : DefaultPieceStacksProviderr
    {
        public TheCavalry()
        {
            HandName = HandName.TheCavalry;
            pieceStacks = new PieceStack[] { new PieceStack(PieceName.Pawn, 1), new PieceStack(PieceName.Archer, 1), new PieceStack(PieceName.Horse, 1) };
        }
    }
}