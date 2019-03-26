using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Application;

namespace Azarashi.CerkeOnline.Presentation.Presenter
{
    public class CurrentTurnPresenter : MonoBehaviour
    {
        [SerializeField] Text currentTurnText = default;

        void Start()
        {
            GameController.Instance.OnGameReset.TakeUntilDestroy(this).Subscribe(OnGameReset);
        }

        void OnGameReset(IGame game)
        {
            this.UpdateAsObservable().TakeUntilDestroy(this)
                .Select(_ => game.CurrentTurn).DistinctUntilChanged().Subscribe(value =>
                {
                    currentTurnText.text = "現在のターン : " + value.ToString();
                });
        }
    }
}