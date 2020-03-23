using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Domain.UseCase;

namespace Azarashi.CerkeOnline.Application
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