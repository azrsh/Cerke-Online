using System;
using Azarashi.CerkeOnline.Domain.Entities;

namespace Azarashi.CerkeOnline.Application
{
    public struct Ruleset
    {
        public readonly int id;
        public readonly string rulesetName;
        readonly IGameInstanceFactory factory;

        public Ruleset(int id, string rulesetName, IGameInstanceFactory factory)
        {   
            this.id = id;
            this.rulesetName = rulesetName ?? throw new ArgumentNullException();
            this.factory = factory ?? throw new ArgumentNullException();
        }

        public IGame CreateGameInstance(Terminologies.Encampment firstPlayerEncampment) => factory.CreateInstance(firstPlayerEncampment);
    }

    public interface IGameInstanceFactory
    {
        IGame CreateInstance(Terminologies.Encampment firstPlayerEncampment);
    }

    public class DefaultGameInstanceFactory<TGame> : IGameInstanceFactory
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