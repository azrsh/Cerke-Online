using UniRx.Async;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    internal interface IPieceMoveTransaction
    {
        UniTask<PieceMoveResult> StartMove();
        void RollBack();
    }
}