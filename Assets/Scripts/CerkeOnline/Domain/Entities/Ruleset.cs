using System;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    public struct Ruleset
    {
        public readonly int id;
        public readonly string name;
        public readonly string description;
        public IGameInstanceFactory Factory { get; }

        public Ruleset(int id, string name, string description, IGameInstanceFactory factory)
        {   
            this.id = id;
            this.name = name ?? throw new ArgumentNullException();
            this.description = description ?? throw new ArgumentNullException();
            Factory = factory ?? throw new ArgumentNullException();
        }
    }
}