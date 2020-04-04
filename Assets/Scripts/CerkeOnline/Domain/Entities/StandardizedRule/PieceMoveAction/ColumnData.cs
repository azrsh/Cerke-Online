using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.PieceMoveAction.DataStructure
{
    internal readonly struct ColumnData
    {
        public PublicDataType.IntegerVector2 Positin { get; }
        public IPiece Piece { get { return pieces.Read(Positin); } }

        readonly PositionArrayAccessor<IPiece> pieces;

        public ColumnData(PublicDataType.IntegerVector2 positin, PositionArrayAccessor<IPiece> pieces)
        {
            Positin = positin;
            this.pieces = pieces;
        }
    }
}