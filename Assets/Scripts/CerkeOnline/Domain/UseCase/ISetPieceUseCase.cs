using UnityEngine;
using Azarashi.CerkeOnline.Domain.Entities;

namespace Azarashi.CerkeOnline.Domain.UseCase
{
    public interface ISetPieceUseCase
    {
        void RequestToSetPiece(Vector2Int position, IPiece piece);
    }
}