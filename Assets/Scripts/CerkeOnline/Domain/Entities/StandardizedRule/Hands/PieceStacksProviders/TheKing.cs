using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.Hands.PieceStackProviders
{
    internal class TheKing : DefaultPieceStacksProviderr
    {
        public TheKing()
        {
            HandName = HandName.TheKing;
            pieceStacks = new PieceStack[] { new PieceStack(PieceName.King, 1) };
        }
    }
}