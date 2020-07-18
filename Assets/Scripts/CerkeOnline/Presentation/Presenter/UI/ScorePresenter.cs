using System;
using UnityEngine;
using UniRx;
using TMPro;
using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Application;
using Azarashi.CerkeOnline.Application.Language;

namespace Azarashi.CerkeOnline.Presentation.Presenter.UI
{
    public class ScorePresenter : MonoBehaviour
    {
        [SerializeField] TMP_Text scoreText = default;
        [SerializeField] Terminologies.FirstOrSecond firstOrSecond = default;
        [SerializeField] TranslatableKeys scoreTextLabel = default;

        void Start()
        {
            GameController.Instance.OnGameReset.TakeUntilDestroy(this).Subscribe(OnGameReset);
            scoreText.enabled = false;
        }

        void OnGameReset(IGame game)
        {
            var scoreUseCase = ScoreeUseCaseFactory.Create(firstOrSecond);
            if (scoreUseCase == null)
            {
                scoreText.enabled = false;
                return;
            }

            Action<Unit> onTurnChanged = _ =>
            {
                var score = scoreUseCase.Score;
                var data = LanguageManager.Instance.Translator.Translate(scoreTextLabel);
                scoreText.text = data.Text + score.ToString();
                scoreText.font = data.FontAsset;
            };

            scoreText.enabled = true;
            game.OnTurnChanged.TakeUntilDestroy(this).Subscribe(onTurnChanged);

            onTurnChanged(Unit.Default);
        }
    }
}