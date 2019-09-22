using System.Linq;
using UnityEngine;
using Azarashi.Utilities.Collections;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.Official
{
    public class FieldEffectChecker : IFieldEffectChecker
    {
        readonly Vector2YXArrayAccessor<FieldEffect> columns;
        readonly IReadOnlyPiece[] pieces;

        public FieldEffectChecker(Vector2YXArrayAccessor<FieldEffect> columns, params IReadOnlyPiece[] pieces)
        {
            this.columns = columns;
            this.pieces = pieces;
        }

        public bool IsInTammua(Vector2Int position)
        {
            return columns.Read(position) == FieldEffect.Tammua || columns.Read(position) == FieldEffect.Tanzo;
        }

        public bool IsInTarfe(Vector2Int position)
        {
            return columns.Read(position) == FieldEffect.Tarfe || columns.Read(position) == FieldEffect.Tanzo || IsInAroundPieces(position);
        }

        bool IsInAroundPieces(Vector2Int position)
        {
            return pieces.Select(piece => piece.Position).All(piecePosition => (piecePosition - position).sqrMagnitude <= 8);
        }

        public bool IsExpandedMoveField(Vector2Int position) => IsInTarfe(position);
    }
}