using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Azarashi.CerkeOnline.Application;
using Azarashi.CerkeOnline.Domain.UseCase;

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
                resultText = "引き分け";
            else if (localPlayerScore > remotePlayerScore)
                resultText = "勝利";
            else
                resultText = "敗北";
            scoreText.text = resultText;
        }
    }
}