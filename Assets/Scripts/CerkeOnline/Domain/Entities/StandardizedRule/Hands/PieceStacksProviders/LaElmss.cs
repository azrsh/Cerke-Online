using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.Hands.PieceStackProviders
{
    internal class LaElmss : DefaultPieceStacksProviderr
    {
        internal LaElmss()
        {
            pieceStacks = new PieceStack[] { new PieceStack(PieceName.Varxle, 1), new PieceStack(PieceName.Elmer, 2) };
        }
    }
}