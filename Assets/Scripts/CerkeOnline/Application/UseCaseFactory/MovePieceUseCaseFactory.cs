using Azarashi.CerkeOnline.Domain.Entities;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;
using Azarashi.CerkeOnline.Domain.UseCase;

namespace Azarashi.CerkeOnline.Application
{
    public static class MovePieceUseCaseFactory
    {
        public static IMovePieceUseCase Create(FirstOrSecond firstOrSecond, IValueInputProvider<int> valueProvider)
        {
            var game = GameController.Instance.Game;
            if (game == null) return null;

            var player = game.GetPlayer(firstOrSecond);
            var logger = GameController.Instance.SystemLogger;
            return new PlayerMovePieceUseCase(game, player, valueProvider, logger.ToUseCaseLogger());
        }
    }
}