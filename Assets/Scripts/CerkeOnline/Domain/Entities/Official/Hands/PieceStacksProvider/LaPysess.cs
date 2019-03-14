using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.Official.Hands
{
    public class LaPysess : DefaultPieceStacksProviderr
    {
        public LaPysess()
        {
            pieceStacks = new PieceStack[] { new PieceStack(PieceName.Stistyst, 1), new PieceStack(PieceName.Dodor, 1) };
        }
    }
}