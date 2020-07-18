using UnityEngine;
using UnityEngine.Assertions;
using UniRx;
using TMPro;
using Azarashi.CerkeOnline.Application;
using Azarashi.CerkeOnline.Application.Language;
using UnityEngine.SocialPlatforms.Impl;

namespace Azarashi.CerkeOnline.Presentation.View.GameResultMenu
{
    public class ScoreResultView : MonoBehaviour
    {
        [SerializeField] TMP_Text scoreText = default;

        void Start()
        {
            Assert.IsNotNull(scoreText);

            GameController.Instance.OnGameReset.TakeUntilDestroy(this).Subscribe(_ => Bind()); 
            if (GameController.Instance.Game != null)
                Bind();
        }

        void Bind()
        {
            //Domain.Entities.Terminologies.FirstOrSecond.Firstをローカルのプレイヤーを指すように変える
            var score  = ScoreeUseCaseFactory.Create(Domain.Entities.Terminologies.FirstOrSecond.First).Score;
            var data = LanguageManager.Instance.Translator.Translate(TranslatableKeys.ScoreLabel);
            scoreText.text = data.Text + score.ToString();
            scoreText.font = data.FontAsset;
        }
    }
}