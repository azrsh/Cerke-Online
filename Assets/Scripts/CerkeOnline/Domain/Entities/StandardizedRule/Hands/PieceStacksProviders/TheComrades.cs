using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.Hands.PieceStackProviders
{
    internal class TheComrades : DefaultPieceStacksProviderr
    {
        public TheComrades()
        {
            HandName = HandName.TheComrades;
            pieceStacks = new PieceStack[] { new PieceStack(PieceName.Vadyrd, 1), new PieceStack(PieceName.Elmer, 2) };
        }
    }
}