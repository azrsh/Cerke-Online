using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Azarashi.CerkeOnline.Application;
using Azarashi.CerkeOnline.Domain.UseCase;

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
            handText.text = "成立した役 : " + hands.Select(hand => hand.Name)
                .DefaultIfEmpty().Aggregate((previous, next) => previous + next + System.Environment.NewLine);
        }
    }
}