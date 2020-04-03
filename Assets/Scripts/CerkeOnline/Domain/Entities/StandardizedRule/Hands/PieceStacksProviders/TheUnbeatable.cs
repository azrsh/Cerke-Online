using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.Hands.PieceStackProviders
{
    internal class TheUnbeatable : DefaultPieceStacksProviderr
    {
        public TheUnbeatable()
        {
            HandName = HandName.TheUnbeatable;
            pieceStacks = new PieceStack[] { new PieceStack(PieceName.Vessel,1), new PieceStack(PieceName.Pawn,1), new PieceStack(PieceName.Archer,1),
                new PieceStack(PieceName.Chariot,1),new PieceStack(PieceName.Tiger,1),new PieceStack(PieceName.Horse,1),
                new PieceStack(PieceName.Officer,1),new PieceStack(PieceName.Shaman,1),new PieceStack(PieceName.General,1),
                new PieceStack(PieceName.King,1),new PieceStack(PieceName.Minds,1)};
        }
    }
}