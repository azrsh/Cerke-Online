using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Azarashi.CerkeOnline.Application;
using Azarashi.CerkeOnline.Application.Language;

namespace Azarashi.CerkeOnline.Presentation.View.GameResultMenu
{
    public class HandResultView : MonoBehaviour
    {
        [SerializeField] Text handText = default;

        void Start()
        {
            GameController.Instance.OnGameReset.TakeUntilDestroy(this).Subscribe(_ => Bind());
            if (GameController.Instance.Game != null)
                Bind();
        }

        void Bind()
        {    //Domain.Entities.Terminologies.FirstOrSecond.Firstをローカルのプレイヤーを指すように変える
            var hands  = HandUseCaseFactory.Create(Domain.Entities.Terminologies.FirstOrSecond.First).GetCurrentHands();
            handText.text = LanguageManager.Instance.Translator.Translate(TranslatableKeys.CompletedHandsLabel) + hands.Select(hand => hand.Name)
                .DefaultIfEmpty().Aggregate((previous, next) => previous + next + System.Environment.NewLine);
        }
    }
}