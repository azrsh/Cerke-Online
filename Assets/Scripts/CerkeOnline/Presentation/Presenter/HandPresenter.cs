using System.Linq;
using UnityEngine;
using UniRx;
using Azarashi.CerkeOnline.Application;
using Azarashi.CerkeOnline.Application.Language;
using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Domain.UseCase;

namespace Azarashi.CerkeOnline.Presentation.Presenter
{
    public class HandPresenter : MonoBehaviour
    {
        IHandUseCase handUseCase;
        UnityEngine.ILogger logger;
        IHand[] notifiedHands = new IHand[0];

        void Start()
        {
            logger = GameController.Instance.SystemLogger;
            GameController.Instance.OnGameReset.TakeUntilDestroy(this).Subscribe(OnGameReset);
        }

        void OnGameReset(IGame game)
        {
            handUseCase = HandUseCaseFactory.Create(Terminologies.FirstOrSecond.First);
            game.OnTurnChanged.TakeUntilDestroy(this).Select(_ => handUseCase.GetHandDifference(notifiedHands))
                .Subscribe(difference => NotifyHandDifference(difference));
            game.SeasonSequencer.OnEnd.TakeUntilDestroy(this).Subscribe(_ => ResetNotifiedHands());
        }

        void NotifyHandDifference(HandDifference handDifference)
        {
            foreach (IHand hand in handDifference.IncreasedDifference)
                logger.Log(
                    LanguageManager.Instance.Translator.Translate(TranslatableKeys.HandCompleteMessage)
                        .Replace("#HAND_NAME#", hand.Name)  //キーワードの置き換えはここでやるべきではない
                );
            foreach (IHand hand in handDifference.DecreasedDifference)
                logger.Log(
                    LanguageManager.Instance.Translator.Translate(TranslatableKeys.HandUncompleteMessage)
                        .Replace("#HAND_NAME#", hand.Name)  //キーワードの置き換えはここでやるべきではない
                );

            notifiedHands = handUseCase.GetCurrentHands().ToArray(); //コピーのためのToArray
        }

        void ResetNotifiedHands()
        {
            notifiedHands = new IHand[0];
        }
    }
}