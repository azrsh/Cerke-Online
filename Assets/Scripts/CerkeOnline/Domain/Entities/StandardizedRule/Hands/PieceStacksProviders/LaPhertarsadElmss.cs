using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.Hands.PieceStackProviders
{
    public class LaPhertarsadElmss : DefaultPieceStacksProviderr
    {
        public LaPhertarsadElmss()
        {
            pieceStacks = new PieceStack[] { new PieceStack(PieceName.Elmer, 5) };
        }
    }
}