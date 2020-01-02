using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Azarashi.CerkeOnline.Application;
using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Domain.UseCase;

namespace Azarashi.CerkeOnline.Presentation.Presenter
{
    public class HandPresenter : MonoBehaviour
    {
        IScoreUseCase scoreUseCase;

        void Start()
        {
            GameController.Instance.OnGameReset.TakeUntilDestroy(this).Subscribe(OnGameReset);
        }

        void OnGameReset(IGame game)
        {
            scoreUseCase = ScoreeUseCaseFactory.Create(Terminologies.FirstOrSecond.First);
            game.OnTurnChanged.TakeUntilDestroy(this).Subscribe(_ => scoreUseCase.LogHandDifference());
        }
    }
}