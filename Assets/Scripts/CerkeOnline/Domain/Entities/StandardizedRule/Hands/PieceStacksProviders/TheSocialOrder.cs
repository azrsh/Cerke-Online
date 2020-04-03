using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.Hands.PieceStackProviders
{
    internal class TheSocialOrder : DefaultPieceStacksProviderr
    {
        public TheSocialOrder()
        {
            HandName = HandName.TheSocialOrder;
            pieceStacks = new PieceStack[] { new PieceStack(PieceName.Pawn,1), new PieceStack(PieceName.Archer,1),
                new PieceStack(PieceName.Officer,1), new PieceStack(PieceName.Shaman,1),new PieceStack(PieceName.General,1)};
        }
    }
}