using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.Hands.PieceStackProviders
{
    internal class TheAttack : DefaultPieceStacksProviderr
    {
        public TheAttack()
        {
            HandName = HandName.TheAttack;
            pieceStacks = new PieceStack[] { new PieceStack(PieceName.Vessel,1), new PieceStack(PieceName.Chariot,1), new PieceStack(PieceName.Horse,1) };
        }
    }
}