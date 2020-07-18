using UniRx;
using UnityEngine;
using TMPro;
using Azarashi.CerkeOnline.Application;
using Azarashi.CerkeOnline.Application.Language;
using Azarashi.CerkeOnline.Domain.Entities;

namespace Azarashi.CerkeOnline.Presentation.Presenter
{
    public class SeasonPresenter : MonoBehaviour
    {
        [SerializeField] TMP_Text seasonText = default;

        void Start()
        {
            GameController.Instance.OnGameReset.TakeUntilDestroy(this).Subscribe(Bind);
        }

        void Bind(IGame game)
        {
            game.SeasonSequencer.OnStart.TakeUntilDestroy(this).Subscribe(season => UpdateSeasonView(season.Season));
            UpdateSeasonView(game.SeasonSequencer.CurrentSeason.Season);    //不格好
        }

        void UpdateSeasonView(Terminologies.Season current)
        {
            var translator = LanguageManager.Instance.Translator;
            var data = translator.Translate(TranslatableKeys.CurrentSeasonLabel);
            seasonText.text = data.Text + SeasonTranslator.Translate(current, translator);
            seasonText.font = data.FontAsset;
        }
    }
}