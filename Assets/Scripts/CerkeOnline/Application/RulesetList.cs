using System.Linq;
using System.Collections.Generic;
using Azarashi.Utilities;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Application
{
    public class RulesetList
    {
        readonly Dictionary<int, Ruleset> rulesets;   
        public RulesetList(IReadOnlyServiceLocator serviceLocator)
        {
            //RulesetNameとの相互依存関係を明確化する
            IGameInstanceFactory[] factories = new IGameInstanceFactory[]
            {
                new DefaultGameInstanceFactory<Domain.Entities.StandardizedRule.StandardizedRuleGame>(firstPlayerEncampment => new Domain.Entities.StandardizedRule.StandardizedRuleGame(firstPlayerEncampment, serviceLocator)),
                new DefaultGameInstanceFactory<Domain.Entities.StandardizedRule.StandardizedRuleGame>(firstPlayerEncampment => new Domain.Entities.StandardizedRule.StandardizedRuleGame(firstPlayerEncampment, serviceLocator)),
                new DefaultGameInstanceFactory<Domain.Entities.NoRule.NoRuleGame>(firstPlayerEncampment => new Domain.Entities.NoRule.NoRuleGame(firstPlayerEncampment)),
            };
            
            int i = 0;
            rulesets = factories.Select(factory => 
            {
                RulesetName rulesetName = (RulesetName)i;
                return new Ruleset(i++, rulesetName.ToString(), "", factory);
            }).ToDictionary(ruleset => ruleset.id);
        }

        public Ruleset GetRuleset(RulesetName rulesetName) => GetRuleset((int)rulesetName);
        
        public Ruleset GetRuleset(int id)
        {
            Ruleset ruleset = default;
            rulesets.TryGetValue(id, out ruleset);
            return ruleset;
        }

        public string[] GetNames() => rulesets.Select(ruleset => ruleset.Value.name).ToArray();
    }
}