namespace Azarashi.CerkeOnline.Domain.Entities
{
    public class OperationStatus
    {
        public int Count { get; private set; }
        public IReadOnlyPiece PreviousPiece { get; private set; }

        public OperationStatus()
        {
            Count = 1;
            PreviousPiece = null;
        }

        public void Reset(IReadOnlyPiece piece)
        {
            Count = 1;
            PreviousPiece = piece;
        }

        public void AddCount()
        {
            Count++;
        }
    }
}