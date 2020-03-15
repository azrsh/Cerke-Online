using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.Hands.PieceStackProviders
{
    public class LaNermetixaler : DefaultPieceStacksProviderr
    {
        public LaNermetixaler()
        {
            pieceStacks = new PieceStack[] { new PieceStack(PieceName.Ales, 1) };
        }
    }
}