﻿using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Azarashi.CerkeOnline.Application;
using Azarashi.CerkeOnline.Domain.Entities;

namespace Azarashi.CerkeOnline.Presentation.Presenter.UI
{
    public class SeasonContinueSelectPresenter : MonoBehaviour, ISeasonDeclarationProvider
    {
        [SerializeField] Canvas seasonContinueSelectCanvas = default;
        [SerializeField] Button quitButton = default;
        [SerializeField] Button continueButton = default;

        public bool IsRequestCompleted { get; private set; }

        void Start()
        {
            //Awake以外からの呼び出し禁止とasをなくす
            //ここ以外に移す
            (GameController.Instance.ServiceLocator as Utilities.IServiceLocator).SetInstance<ISeasonDeclarationProvider>(this);
            
            Close();
        }

        void Open() => seasonContinueSelectCanvas.gameObject.SetActive(true);

        void Close() => seasonContinueSelectCanvas.gameObject.SetActive(false);

        public void RequestValue(Action<SeasonContinueOrEnd> callback)
        {
            IsRequestCompleted = false;

            Action<SeasonContinueOrEnd> action = continueOrEnd =>
            {
                Close();
                GameController.Instance.SystemLogger.Log(continueOrEnd);
                callback(continueOrEnd);
                IsRequestCompleted = true;
            };

            Open();
            quitButton.OnClickAsObservable().First().TakeUntilDestroy(this).Subscribe(_ => action(SeasonContinueOrEnd.End));
            continueButton.OnClickAsObservable().First().TakeUntilDestroy(this).Subscribe(_ => action(SeasonContinueOrEnd.Continue));
        }
    }
}