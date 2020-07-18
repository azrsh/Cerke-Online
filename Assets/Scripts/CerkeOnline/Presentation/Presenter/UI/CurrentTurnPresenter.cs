using UnityEngine;
using UniRx;
using UniRx.Triggers;
using TMPro;
using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Application;
using Azarashi.CerkeOnline.Application.Language;

namespace Azarashi.CerkeOnline.Presentation.Presenter.UI
{
    public class CurrentTurnPresenter : MonoBehaviour
    {
        [SerializeField] TMP_Text currentTurnText = default;

        void Start()
        {
            GameController.Instance.OnGameReset.TakeUntilDestroy(this).Subscribe(OnGameReset);
        }

        void OnGameReset(IGame game)
        {
            this.UpdateAsObservable().TakeUntilDestroy(this)
                .Select(_ => game.CurrentTurn).DistinctUntilChanged().Subscribe(value =>
                {
                    var translator = LanguageManager.Instance.Translator;
                    var data = translator.Translate(TranslatableKeys.CurrentPlayerLabel);
                    currentTurnText.text = data.Text
                    + FirstOrSecondTranslator.Translate(value, translator);
                    currentTurnText.font = data.FontAsset;
                });
        }
    }
}