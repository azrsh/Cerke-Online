using System.Linq;
using UnityEngine;
using UniRx;
using TMPro;
using Azarashi.CerkeOnline.Application;
using Azarashi.CerkeOnline.Application.Language;

namespace Azarashi.CerkeOnline.Presentation.View.GameResultMenu
{
    public class HandResultView : MonoBehaviour
    {
        [SerializeField] TMP_Text handText = default;

        void Start()
        {
            GameController.Instance.OnGameReset.TakeUntilDestroy(this).Subscribe(_ => Bind());
            if (GameController.Instance.Game != null)
                Bind();

            // TODO: 途中でフォントを切り替えるとこれまでの表示が文字化けする可能性があるので修正する
            LanguageManager.Instance.OnLanguageChanged.Subscribe(translator => 
                handText.font = translator.Translate(TranslatableKeys.Hand).FontAsset
            );
        }

        void Bind()
        {    //Domain.Entities.Terminologies.FirstOrSecond.Firstをローカルのプレイヤーを指すように変える
            var hands  = HandUseCaseFactory.Create(Domain.Entities.Terminologies.FirstOrSecond.First).GetCurrentHands();
            handText.text = hands.Select(hand => hand.Name).Select(name => HandNameTranslator.Translate(name, LanguageManager.Instance.Translator))
                .DefaultIfEmpty().Aggregate((previous, next) => previous + next + System.Environment.NewLine);
        }
    }
}