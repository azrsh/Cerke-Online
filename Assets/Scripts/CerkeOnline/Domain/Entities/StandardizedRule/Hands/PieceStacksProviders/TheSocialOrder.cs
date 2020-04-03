using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.Hands.PieceStackProviders
{
    internal class TheSocialOrder : DefaultPieceStacksProviderr
    {
        public TheSocialOrder()
        {
            HandName = HandName.TheSocialOrder;
            pieceStacks = new PieceStack[] { new PieceStack(PieceName.Elmer,1), new PieceStack(PieceName.Gustuer,1),
                new PieceStack(PieceName.Kua,1), new PieceStack(PieceName.Terlsk,1),new PieceStack(PieceName.Varxle,1)};
        }
    }
}