using System;
using UniRx;
using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;
using Azarashi.CerkeOnline.Application;
using Azarashi.CerkeOnline.Application.Language;
using Azarashi.CerkeOnline.Domain.Entities;

namespace Azarashi.CerkeOnline.Presentation.Presenter.UI
{
    public class SeasonContinueSelectPresenter : MonoBehaviour, ISeasonDeclarationProvider
    {
        [SerializeField] Canvas seasonContinueSelectCanvas = default;
        [SerializeField] Button quitButton = default;
        [SerializeField] Button continueButton = default;

        void Start()
        {
            //Awake以外からの呼び出し禁止とasをなくす
            //ここ以外に移す
            (GameController.Instance.ServiceLocator as Utilities.IServiceLocator).SetInstance<ISeasonDeclarationProvider>(this);
            
            Close();
        }

        void Open() => seasonContinueSelectCanvas.gameObject.SetActive(true);

        void Close() => seasonContinueSelectCanvas.gameObject.SetActive(false);

        public async UniTask<SeasonContinueOrEnd> RequestValue()
        {
            Open();
            var quitButtonAsObservable = quitButton.OnClickAsObservable().TakeUntilDestroy(this).Select(_ => SeasonContinueOrEnd.End);
            var continueButtonAsObservable = continueButton.OnClickAsObservable().TakeUntilDestroy(this).Select(_ => SeasonContinueOrEnd.Continue);
            
            var result = await quitButtonAsObservable.Merge(continueButtonAsObservable).First();

            Close();
            TranslatableKeys key = result == SeasonContinueOrEnd.Continue ? TranslatableKeys.DeclaringToContinueButton : TranslatableKeys.DeclaringToEndButton;
            var message = LanguageManager.Instance.Translator.Translate(key);
            GameController.Instance.SystemLogger.Log(message);

            return result;
        }
    }
}