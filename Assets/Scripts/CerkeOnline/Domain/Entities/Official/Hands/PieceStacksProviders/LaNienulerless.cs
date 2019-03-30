using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.Official.Hands.PieceStackProviders
{
    public class LaNienulerless : DefaultPieceStacksProviderr
    {
        public LaNienulerless()
        {
            pieceStacks = new PieceStack[] { new PieceStack(PieceName.Felkana,1), new PieceStack(PieceName.Vadyrd,1), new PieceStack(PieceName.Dodor,1) };
        }
    }
}