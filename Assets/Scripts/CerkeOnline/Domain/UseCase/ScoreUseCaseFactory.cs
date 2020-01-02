using Azarashi.CerkeOnline.Application;
using Azarashi.CerkeOnline.Domain.Entities;

namespace Azarashi.CerkeOnline.Domain.UseCase
{
    public static class ScoreeUseCaseFactory
    {
        public static IScoreUseCase Create(Terminologies.FirstOrSecond firstOrSecond)
        {
            var game = GameController.Instance.Game;
            var handDatabase = game.HandDatabase;
            if (game == null || handDatabase == null) return null;

            var self = game.GetPlayer(firstOrSecond);
            var opponent = game.GetPlayer(Terminologies.GetReversal(firstOrSecond));
            var logger = GameController.Instance.SystemLogger;
            return new ScoreUseCase(self, opponent, handDatabase, game.ScoreHolder, logger);
        }
    }
}