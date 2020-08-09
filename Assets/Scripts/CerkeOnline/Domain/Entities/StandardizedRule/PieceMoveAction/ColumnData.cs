using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.PieceMoveAction.DataStructure
{
    internal readonly struct ColumnData
    {
        public IntegerVector2 Positin { get; }
        public IPiece Piece { get { return pieces.Read(Positin); } }

        readonly IReadOnlyPositionArrayAccessor<IPiece> pieces;

        public ColumnData(IntegerVector2 positin, IReadOnlyPositionArrayAccessor<IPiece> pieces)
        {
            Positin = positin;
            this.pieces = pieces;
        }
    }
}