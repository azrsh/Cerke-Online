using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Azarashi.CerkeOnline.Application;
using Azarashi.CerkeOnline.Application.Language;
using Azarashi.CerkeOnline.Domain.Entities;

namespace Azarashi.CerkeOnline.Presentation.Presenter
{
    public class SeasonPresenter : MonoBehaviour
    {
        [SerializeField] Text seasonText = default;

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
            seasonText.text = LanguageManager.Instance.Translator.Translate(TranslatableKeys.CurrentSeasonLabel)
                + Terminologies.SeasonDictionary.EnumToJapanese[current];
        }
    }
}