using UniRx.Async;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    internal interface IPieceMoveAction
    {
        UniTask<PieceMoveResult> StartMove();
        void RollBack();
    }
}