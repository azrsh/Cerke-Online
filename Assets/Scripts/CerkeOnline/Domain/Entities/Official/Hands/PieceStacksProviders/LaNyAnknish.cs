using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.Official.Hands.PieceStackProviders
{
    public class LaNyAnknish : DefaultPieceStacksProviderr
    {
        public LaNyAnknish()
        {
            pieceStacks = new PieceStack[] { new PieceStack(PieceName.Elmer,1), new PieceStack(PieceName.Gustuer,1),
                new PieceStack(PieceName.Kua,1), new PieceStack(PieceName.Terlsk,1),new PieceStack(PieceName.Varxle,1)};
        }
    }
}