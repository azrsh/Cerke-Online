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

            var player = game.GetPlayer(firstOrSecond);
            return new ScoreUseCase(player, game.ScoreHolder);
        }
    }
}