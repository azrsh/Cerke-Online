using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Azarashi.CerkeOnline.Application;
using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Domain.UseCase;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Presentation.Presenter.Columns
{
    [RequireComponent(typeof(IValueInputProvider<int>))]
    public class OfflineColumnSelector : BaseColumnSelector
    {
        // 現在はColumnSelectorを継承しているが, ColumnSelectorの内部処理は分離してPureC#にした方がいいかも

        private FirstOrSecond firstOrSecond = default;
        
        IValueInputProvider<int> valueProvider;
        
        protected void Start()
        {   
            valueProvider = GetComponent<IValueInputProvider<int>>();
            GameController.Instance.Game.OnTurnChanged.TakeUntilDestroy(this).Subscribe(OnTurnChanged);
            this.UpdateAsObservable().TakeUntilDestroy(this).Select(_ => valueProvider.IsRequestCompleted).DistinctUntilChanged().Subscribe(value => isLockSelecting = !value);

            isLockSelecting = !valueProvider.IsRequestCompleted;
            OnTurnChanged(Unit.Default);
        }

        void OnTurnChanged(Unit unit)
        {
            firstOrSecond = GameController.Instance.Game.CurrentTurn;
        }

        protected override void OnColumnSelected(Vector2Int start, Vector2Int end)
        {
            MovePieceUseCaseFactory.Create(firstOrSecond, valueProvider).RequestToMovePiece(start, end);
        }
    }
}