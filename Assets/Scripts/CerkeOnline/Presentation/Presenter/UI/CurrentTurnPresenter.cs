using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Application;
using Azarashi.CerkeOnline.Application.Language;

namespace Azarashi.CerkeOnline.Presentation.Presenter.UI
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
                    currentTurnText.text = LanguageManager.Instance.Translator.Translate(TranslatableKeys.CurrentPlayerLabel) + value.ToString();
                });
        }
    }
}