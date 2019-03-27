using Azarashi.CerkeOnline.Application;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.UseCase
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