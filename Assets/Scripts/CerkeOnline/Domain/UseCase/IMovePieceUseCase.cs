using UnityEngine;

namespace Azarashi.CerkeOnline.Domain.UseCase
{
    public interface IMovePieceUseCase
    {
        void RequestToMovePiece(Vector2Int start, Vector2Int via, Vector2Int last);
        void RequestToMovePiece(Vector2Int start, Vector2Int last);
    }
}