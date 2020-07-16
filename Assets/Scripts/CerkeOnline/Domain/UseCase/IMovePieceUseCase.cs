using UniRx.Async;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;

namespace Azarashi.CerkeOnline.Domain.UseCase
{
    public interface IMovePieceUseCase
    {
        UniTask RequestToMovePiece(IntegerVector2 start, IntegerVector2 via, IntegerVector2 last);
        UniTask RequestToMovePiece(IntegerVector2 start, IntegerVector2 last);
    }
}