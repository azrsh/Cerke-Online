using UnityEngine;
using UniRx;
using UniRx.Async;
using Azarashi.CerkeOnline.Application;
using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;
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
            
            GameController.Instance.OnGameReset.TakeUntilDestroy(this).Subscribe(OnGameReset);
        }

        void OnGameReset(IGame game)
        {
            game.OnTurnChanged.TakeUntilDestroy(this).Subscribe(OnTurnChanged);
            isLockSelecting = false;
            OnTurnChanged(Unit.Default);
        }

        void OnTurnChanged(Unit unit)
        {
            firstOrSecond = GameController.Instance.Game.CurrentTurn;
        }

        protected override void OnColumnSelected(IntegerVector2 start, IntegerVector2 via, IntegerVector2 last)
        {
            UniTaskVoid nowait = RequestToMove(start, via, last);
        }

        async UniTaskVoid RequestToMove(IntegerVector2 start, IntegerVector2 via, IntegerVector2 last)
        {
            isLockSelecting = true;
            if (via == last)
                await MovePieceUseCaseFactory.Create(firstOrSecond, valueProvider).RequestToMovePiece(start, last);
            else
                await MovePieceUseCaseFactory.Create(firstOrSecond, valueProvider).RequestToMovePiece(start, via, last);

            isLockSelecting = false;
        }
    }
}