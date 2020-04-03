using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.Hands.PieceStackProviders
{
    internal class TheDeadlyArmy : DefaultPieceStacksProviderr
    {
        public TheDeadlyArmy()
        {
            HandName = HandName.TheDeadlyArmy;
            pieceStacks = new PieceStack[] { new PieceStack(PieceName.Pawn, 5) };
        }
    }
}