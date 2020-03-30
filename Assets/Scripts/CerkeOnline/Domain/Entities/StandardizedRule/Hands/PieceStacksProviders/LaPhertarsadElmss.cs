using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.Hands.PieceStackProviders
{
    internal class LaPhertarsadElmss : DefaultPieceStacksProviderr
    {
        internal LaPhertarsadElmss()
        {
            pieceStacks = new PieceStack[] { new PieceStack(PieceName.Elmer, 5) };
        }
    }
}