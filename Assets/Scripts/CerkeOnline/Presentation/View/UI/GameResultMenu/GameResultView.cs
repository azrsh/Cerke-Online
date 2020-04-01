﻿using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Azarashi.CerkeOnline.Application;
using Azarashi.CerkeOnline.Application.Language;

namespace Azarashi.CerkeOnline.Presentation.View.GameResultMenu
{
    public class GameResultView : MonoBehaviour
    {
        [SerializeField] Text scoreText = default;

        void Start()
        {
            GameController.Instance.OnGameReset.TakeUntilDestroy(this).Subscribe(_ => Bind());
            if (GameController.Instance.Game != null)
                Bind();
        }

        void Bind()
        {
            //Domain.Entities.Terminologies.FirstOrSecond.Firstをローカルのプレイヤーを指すように変える
            var localPlayerScore  = ScoreeUseCaseFactory.Create(Domain.Entities.Terminologies.FirstOrSecond.First).Score;
            var remotePlayerScore  = ScoreeUseCaseFactory.Create(Domain.Entities.Terminologies.FirstOrSecond.Second).Score;
            string resultText;
            if (localPlayerScore == remotePlayerScore)
                resultText = LanguageManager.Instance.Translator.Translate(TranslatableKeys.DrawMessage);
            else if (localPlayerScore > remotePlayerScore)
                resultText = LanguageManager.Instance.Translator.Translate(TranslatableKeys.WinMessage);
            else
                resultText = LanguageManager.Instance.Translator.Translate(TranslatableKeys.LoseMessage);
            scoreText.text = resultText;
        }
    }
}