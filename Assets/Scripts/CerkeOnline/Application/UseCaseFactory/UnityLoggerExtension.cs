using Azarashi.CerkeOnline.Domain.UseCase;

namespace Azarashi.CerkeOnline.Application
{
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