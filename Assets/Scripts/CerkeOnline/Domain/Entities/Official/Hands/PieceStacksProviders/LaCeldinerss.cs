using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.Official.Hands.PieceStackProviders
{
    public class LaCeldinerss : DefaultPieceStacksProviderr
    {
        public LaCeldinerss()
        {
            pieceStacks = new PieceStack[] { new PieceStack(PieceName.Vadyrd, 1), new PieceStack(PieceName.Elmer, 2) };
        }
    }
}