using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;

namespace Azarashi.CerkeOnline.Domain.UseCase
{
    public interface IMovePieceUseCase
    {
        void RequestToMovePiece(IntVector2 start, IntVector2 via, IntVector2 last);
        void RequestToMovePiece(IntVector2 start, IntVector2 last);
    }
}