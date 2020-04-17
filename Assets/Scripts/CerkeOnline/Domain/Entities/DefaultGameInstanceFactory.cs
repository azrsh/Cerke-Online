using System;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    internal class DefaultGameInstanceFactory<TGame> : IGameInstanceFactory
        where TGame : IGame
    {
        readonly Func<Terminologies.Encampment, IGame> constructor;

        public DefaultGameInstanceFactory(Func<Terminologies.Encampment,IGame> constructor)
        {
            this.constructor = constructor;
        }

        public IGame CreateInstance(Terminologies.Encampment firstPlayerEncampment) => constructor(firstPlayerEncampment);
    }
}