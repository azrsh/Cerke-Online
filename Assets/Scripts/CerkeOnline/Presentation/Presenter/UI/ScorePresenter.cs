using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Domain.UseCase;
using Azarashi.CerkeOnline.Application;

namespace Azarashi.CerkeOnline.Presentation.Presenter.UI
{
    public class ScorePresenter : MonoBehaviour
    {
        [SerializeField] Text scoreText = default;

        void Start()
        {
            GameController.Instance.OnGameReset.TakeUntilDestroy(this).Subscribe(OnGameReset);
            scoreText.enabled = false;
        }

        void OnGameReset(IGame game)
        {
            var scoreUseCase = ScoreeUseCaseFactory.Create(Terminologies.FirstOrSecond.First);
            if (scoreUseCase == null)
            {
                scoreText.enabled = false;
                return;
            }

            scoreText.enabled = true;
            game.OnTurnChanged.TakeUntilDestroy(this).Subscribe(_ =>
                {
                    var score = scoreUseCase.GetScore();
                    scoreText.text = "現在のスコア : " + score.ToString();
                });
        }
    }
}