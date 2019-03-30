using Azarashi.CerkeOnline.Application;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.UseCase
{
    public static class ScoreeUseCaseFactory
    {
        public static IScoreUseCase Create(FirstOrSecond firstOrSecond)
        {
            var game = GameController.Instance.Game;
            if (game == null) return null;

            var player = game.GetPlayer(firstOrSecond);
            var handDatabase = new Entities.Official.HandDatabase();
            var logger = GameController.Instance.SystemLogger;
            return new ScoreUseCase(player, handDatabase, logger);
        }
    }
}