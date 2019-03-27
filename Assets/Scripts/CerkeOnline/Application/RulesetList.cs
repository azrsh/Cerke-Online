using System;
using System.Linq;
using System.Collections.Generic;
using Azarashi.CerkeOnline.Domain.Entities;

namespace Azarashi.CerkeOnline.Application
{
    public class RulesetList
    {
        readonly Dictionary<int, Ruleset> rulesets;   
        public RulesetList()
        {
            IGameInstanceFactory[] factories = new IGameInstanceFactory[]
            {
                new DefaultGameInstanceFactory<Domain.Entities.NoRule.NoRuleGame>(firstPlayerEncampment => new Domain.Entities.NoRule.NoRuleGame(firstPlayerEncampment)),
                new DefaultGameInstanceFactory<Domain.Entities.Official.OfficialRuleGame>(firstPlayerEncampment => new Domain.Entities.Official.OfficialRuleGame(firstPlayerEncampment)),
            };

            string[] names = new string[]
            {
                    "No Rule",
                    "Official Rule"
            };

            int id = 0;
            rulesets = factories.Select(factory => new Ruleset(id++, names[id - 1], factory)).ToDictionary(ruleset => ruleset.id);
        }

        public Ruleset GetRuleset(int id)
        {
            Ruleset ruleset = default;
            rulesets.TryGetValue(id, out ruleset);
            return ruleset;
        }
    }
}