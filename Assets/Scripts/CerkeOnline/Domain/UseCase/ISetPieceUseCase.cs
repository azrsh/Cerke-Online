using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;

namespace Azarashi.CerkeOnline.Domain.UseCase
{
    public interface ISetPieceUseCase
    {
        void RequestToSetPiece(IntVector2 position, IPiece piece);
    }
}