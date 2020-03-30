using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;

namespace Azarashi.CerkeOnline.Domain.UseCase
{
    public interface ISetPieceUseCase
    {
        void RequestToSetPiece(IntegerVector2 position, IPiece piece);
    }
}