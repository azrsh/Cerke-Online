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
        [SerializeField] Text currentTurnText = default;

        void Start()
        {
            GameController.Instance.OnGameReset.TakeUntilDestroy(this).Subscribe(OnGameReset);
        }

        void OnGameReset(IGame game)
        {
            game.OnTurnChanged.TakeUntilDestroy(this).Subscribe(_ =>
                {
                    var scoreUseCase = ScoreeUseCaseFactory.Create(Terminologies.FirstOrSecond.First);
                    var score = scoreUseCase.GetScore();
                    currentTurnText.text = "現在のスコア : " + score.ToString();
                });
        }
    }
}