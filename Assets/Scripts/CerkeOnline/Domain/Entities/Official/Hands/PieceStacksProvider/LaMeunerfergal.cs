using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.Official.Hands.PieceProviders
{
    public class LaMeunerfergal : DefaultPieceStacksProviderr
    {
        public LaMeunerfergal()
        {
            pieceStacks = new PieceStack[] { new PieceStack(PieceName.Kua, 1), new PieceStack(PieceName.Terlsk, 1), new PieceStack(PieceName.Varxle, 1) };
        }
    }
}