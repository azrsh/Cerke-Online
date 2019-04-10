using UnityEngine;
using Azarashi.Utilities.Collections;

namespace Azarashi.CerkeOnline.Domain.Entities.Official.PieceMoveAction.DataStructure
{
    public struct ColumnData
    {
        public Vector2Int Positin { get; }
        public IPiece Piece { get { return pieces.Read(Positin); } }

        readonly Vector2ArrayAccessor<IPiece> pieces;

        public ColumnData(Vector2Int positin, Vector2ArrayAccessor<IPiece> pieces)
        {
            Positin = positin;
            this.pieces = pieces;
        }
    }
}