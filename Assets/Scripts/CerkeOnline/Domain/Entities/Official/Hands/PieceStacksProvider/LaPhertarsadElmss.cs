using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.Official.Hands
{
    public class LaPhertarsadElmss : DefaultPieceStacksProviderr
    {
        public LaPhertarsadElmss()
        {
            pieceStacks = new PieceStack[] { new PieceStack(PieceName.Elmer, 5) };
        }
    }
}