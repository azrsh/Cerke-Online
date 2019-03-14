using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.Official.Hands.PieceProviders
{
    public class LaAls : DefaultPieceStacksProviderr
    {
        public LaAls()
        {
            pieceStacks = new PieceStack[] { new PieceStack(PieceName.Felkana,1), new PieceStack(PieceName.Elmer,1), new PieceStack(PieceName.Gustuer,1),
                new PieceStack(PieceName.Vadyrd,1),new PieceStack(PieceName.Stistyst,1),new PieceStack(PieceName.Dodor,1),
                new PieceStack(PieceName.Kua,1),new PieceStack(PieceName.Terlsk,1),new PieceStack(PieceName.Varxle,1),
                new PieceStack(PieceName.Ales,1),new PieceStack(PieceName.Tam,1)};
        }
    }
}