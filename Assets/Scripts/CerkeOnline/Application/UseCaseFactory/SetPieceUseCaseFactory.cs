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
            return new PlayerSetPieceUseCase(game, player, logger.ToUseCaseLogger());
        }
    }

    internal static class UnityLoggerExtension
    {
        public static ILogger ToUseCaseLogger(this UnityEngine.ILogger logger)
        {
            return new LoaggerWrapper(logger);
        }

        private class LoaggerWrapper : Domain.UseCase.ILogger
        {
            readonly UnityEngine.ILogger logger;

            public LoaggerWrapper(UnityEngine.ILogger logger)
            {
                this.logger = logger;
            }

            public void Log(string message) => logger.Log(message);
        }
    }
}