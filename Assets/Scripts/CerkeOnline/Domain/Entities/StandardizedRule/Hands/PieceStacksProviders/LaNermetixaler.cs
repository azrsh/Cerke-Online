using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.Hands.PieceStackProviders
{
    internal class LaNermetixaler : DefaultPieceStacksProviderr
    {
        internal LaNermetixaler()
        {
            pieceStacks = new PieceStack[] { new PieceStack(PieceName.Ales, 1) };
        }
    }
}