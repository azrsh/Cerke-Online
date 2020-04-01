﻿using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Azarashi.CerkeOnline.Application;
using Azarashi.CerkeOnline.Application.Language;

namespace Azarashi.CerkeOnline.Presentation.View.GameResultMenu
{
    public class ScoreResultView : MonoBehaviour
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
            var score  = ScoreeUseCaseFactory.Create(Domain.Entities.Terminologies.FirstOrSecond.First).Score;
            scoreText.text = LanguageManager.Instance.Translator.Translate(TranslatableKeys.ScoreLabel) + score.ToString();
        }
    }
}