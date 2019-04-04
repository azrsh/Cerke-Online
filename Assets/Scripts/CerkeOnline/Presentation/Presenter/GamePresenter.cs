using UnityEngine;
using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Domain.Entities.Official;
using Azarashi.CerkeOnline.Domain.UseCase;

namespace Azarashi.CerkeOnline.Presentation.Presenter
{
    /// <summary>
    /// Gameは対局の意味.
    /// </summary>
    public class GamePresenter : MonoBehaviour
    {
        IScoreUseCase scoreUseCase;

        void Start()
        {
            //IGame game = new OfficialRuleGame();
            //scoreUseCase = new ScoreUseCase(game.FirstPlayer, null);
        }

        void Update()
        {

        }
    }
}