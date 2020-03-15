using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.Hands.PieceStackProviders
{
    public class LaVefisait : DefaultPieceStacksProviderr
    {
        public LaVefisait()
        {
            pieceStacks = new PieceStack[] { new PieceStack(PieceName.Elmer, 1), new PieceStack(PieceName.Gustuer, 1), new PieceStack(PieceName.Dodor, 1) };
        }
    }
}