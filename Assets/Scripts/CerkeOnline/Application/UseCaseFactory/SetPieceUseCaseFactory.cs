using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;
using Azarashi.CerkeOnline.Domain.UseCase;

namespace Azarashi.CerkeOnline.Application
{
    public static class SetPieceUseCaseFactory
    {
        public static ISetPieceUseCase Create(FirstOrSecond firstOrSecond)
        {
            var game = GameController.Instance.Game;
            if (game == null) return null;

            var player = game.GetPlayer(firstOrSecond);
            var logger = GameController.Instance.SystemLogger;
            return new PlayerSetPieceUseCase(game, player, logger);
        }
    }
}