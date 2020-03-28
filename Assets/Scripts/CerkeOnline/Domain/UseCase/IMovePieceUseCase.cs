using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;

namespace Azarashi.CerkeOnline.Domain.UseCase
{
    public interface IMovePieceUseCase
    {
        void RequestToMovePiece(IntegerVector2 start, IntegerVector2 via, IntegerVector2 last);
        void RequestToMovePiece(IntegerVector2 start, IntegerVector2 last);
    }
}