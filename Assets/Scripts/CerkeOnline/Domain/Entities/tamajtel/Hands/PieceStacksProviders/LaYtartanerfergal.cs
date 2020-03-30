using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.tamajtel.Hands.PieceStackProviders
{
    internal class LaYtartanerfergal : DefaultPieceStacksProviderr
    {
        internal LaYtartanerfergal()
        {
            pieceStacks = new PieceStack[] { new PieceStack(PieceName.None, 10)};
        }
    }
}