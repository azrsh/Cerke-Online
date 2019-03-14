using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.Official.Hands.PieceStackProvider
{
    public class LaYtartanerfergal : DefaultPieceStacksProviderr
    {
        public LaYtartanerfergal()
        {
            pieceStacks = new PieceStack[] { new PieceStack(PieceName.None, 10)};
        }
    }
}