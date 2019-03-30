using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.Official.Hands.PieceStackProviders
{
    public class LaElmss : DefaultPieceStacksProviderr
    {
        public LaElmss()
        {
            pieceStacks = new PieceStack[] { new PieceStack(PieceName.Varxle, 1), new PieceStack(PieceName.Elmer, 2) };
        }
    }
}