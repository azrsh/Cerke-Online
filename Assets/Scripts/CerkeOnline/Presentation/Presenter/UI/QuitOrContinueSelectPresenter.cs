using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Azarashi.CerkeOnline.Application;
using Azarashi.CerkeOnline.Domain.Entities;

namespace Azarashi.CerkeOnline.Presentation.Presenter
{
    public class QuitOrContinueSelectPresenter : MonoBehaviour
    {
        [SerializeField] Canvas seasonContinueSelectCanvas = default;
        [SerializeField] Button quitButton = default;
        [SerializeField] Button continueButton = default;

        public bool IsRequestCompleted { get; private set; }

        void Start()
        {
            GameController.Instance.OnGameReset.TakeUntilDestroy(this).Subscribe(Bind);
            Close();
        }

        void Bind(IGame game)
        {
            //GameController.Instance.;
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