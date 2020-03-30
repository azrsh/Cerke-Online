using System.Linq;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule
{
    internal class FieldEffectChecker : IFieldEffectChecker
    {
        readonly PositionArrayAccessor<FieldEffect> columns;
        readonly IReadOnlyPiece[] pieces;

        public FieldEffectChecker(PositionArrayAccessor<FieldEffect> columns, params IReadOnlyPiece[] pieces)
        {
            this.columns = columns;
            this.pieces = pieces;
        }

        public bool IsInTammua(PublicDataType.IntegerVector2 position)
        {
            return columns.Read(position) == FieldEffect.Tammua || columns.Read(position) == FieldEffect.Tanzo;
        }

        public bool IsInTarfe(PublicDataType.IntegerVector2 position)
        {
            return columns.Read(position) == FieldEffect.Tarfe || columns.Read(position) == FieldEffect.Tanzo || IsInAroundPieces(position);
        }

        bool IsInAroundPieces(PublicDataType.IntegerVector2 position)
        {
            return pieces.Select(piece => piece.Position).All(piecePosition => (piecePosition - position).sqrMagnitude <= 8);
        }

        public bool IsExpandedMoveField(PublicDataType.IntegerVector2 position) => IsInTarfe(position);
    }
}