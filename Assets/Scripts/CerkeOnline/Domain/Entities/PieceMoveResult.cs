namespace Azarashi.CerkeOnline.Domain.Entities
{
    public readonly struct PieceMoveResult
    {
        public readonly bool isSuccess;
        public readonly bool isTurnEnd;
        public readonly IPiece gottenPiece;

        public PieceMoveResult(bool isSuccess, bool isTurnEnd, IPiece gottenPiece)
        {
            this.isSuccess = isSuccess;
            this.isTurnEnd = isTurnEnd;
            this.gottenPiece = gottenPiece;
        }
    }
}