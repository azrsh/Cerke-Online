using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    public class DefaultScoreHolder : IScoreHolder
    {
        readonly Dictionary<IPlayer, IntReactiveProperty> scores;

        public DefaultScoreHolder(Dictionary<IPlayer, int> scores)
        {
            this.scores = scores.ToDictionary(pair => pair.Key, pair => new IntReactiveProperty(pair.Value));
        }

        /// <summary>
        /// fromからtoへ得点を移動する. 得点は0を下限としており、それ以下にはならない.
        /// </summary>
        /// <param name="scorer">得点を獲得したプレイヤー</param>
        /// <param name="score">移動してほしい得点</param>
        /// <returns>実際に移動した得点</returns>
        public int MoveScore(IPlayer scorer, int score)
        {
            if (!scores.ContainsKey(scorer)) return 0;

            var from = scores.Keys.Where(item => item != scorer).First();
            var to = scorer;

            if (scores[from].Value - score < 0) score = scores[from].Value;
            if (scores[to].Value + score < 0) score = -scores[to].Value;

            scores[from].Value -= score;
            scores[to].Value += score;
            return score;
        }

        public bool TryGetScore(IPlayer player, out IntReactiveProperty score) => scores.TryGetValue(player, out score);
        public bool Contains(IPlayer player) => scores.ContainsKey(player);
        public IReadOnlyReactiveProperty<int> GetScore(IPlayer player) => scores[player];
    }
}