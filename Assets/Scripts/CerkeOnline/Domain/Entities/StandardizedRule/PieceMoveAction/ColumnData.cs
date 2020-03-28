using UnityEngine;
using Azarashi.Utilities.Collections;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.PieceMoveAction.DataStructure
{
    internal struct ColumnData
    {
        public Vector2Int Positin { get; }
        public IPiece Piece { get { return pieces.Read(Positin); } }

        readonly Vector2YXArrayAccessor<IPiece> pieces;

        public ColumnData(Vector2Int positin, Vector2YXArrayAccessor<IPiece> pieces)
        {
            Positin = positin;
            this.pieces = pieces;
        }
    }
}