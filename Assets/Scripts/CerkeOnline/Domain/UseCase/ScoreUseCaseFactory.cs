using Azarashi.CerkeOnline.Application;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.UseCase
{
    public static class ScoreeUseCaseFactory
    {
        public static IScoreUseCase Create(FirstOrSecond firstOrSecond)
        {
            var game = GameController.Instance.Game;
            var handDatabase = game.HandDatabase;
            if (game == null || handDatabase == null) return null;

            var player = game.GetPlayer(firstOrSecond);
            var logger = GameController.Instance.SystemLogger;
            return new ScoreUseCase(player, game.ScoreHolder);
        }
    }
}