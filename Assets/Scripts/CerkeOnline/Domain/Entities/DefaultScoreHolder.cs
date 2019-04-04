using System.Collections.Generic;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    public class DefaultScoreHolder : IScoreHolder
    {
        readonly Dictionary<IPlayer, int> scores;

        public DefaultScoreHolder(Dictionary<IPlayer, int> scores)
        {
            this.scores = scores;
        }

        /// <summary>
        /// fromからtoへ得点を移動する. 得点は0を下限としており、それ以下にはならない.
        /// </summary>
        /// <param name="from">得点の移動元のプレイヤー</param>
        /// <param name="to">得点の移動先のプレイヤー</param>
        /// <param name="score">移動してほしい得点</param>
        /// <returns>実際に移動した得点</returns>
        public int MoveScore(IPlayer from, IPlayer to, int score)
        {
            if (!scores.ContainsKey(from) || !scores.ContainsKey(to)) return 0;

            if (scores[from] - score < 0) score = scores[from];
            if (scores[to] + score < 0) score = -scores[to];

            scores[from] += score;
            scores[to] -= score;
            return score;
        }

        public bool TryGetScore(IPlayer player, out int score) => scores.TryGetValue(player, out score);
        public bool Contains(IPlayer player) => scores.ContainsKey(player);
        public int GetScore(IPlayer player) => scores[player];
    }
}