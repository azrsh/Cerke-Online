using System.Linq;
using UnityEngine;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    /// <summary>
    /// Playerの手駒を参照し, その時終季した場合に得られる得点を計算する.
    /// </summary>
    public class ExpectedChangeScoreCalculator
    {
        readonly IPlayer player;
        readonly IHandDatabase handDatabase;
        readonly ILogger logger;

        IHand[] previousHands;

        public ExpectedChangeScoreCalculator(IPlayer player, IHandDatabase handDatabase, ILogger logger)
        {
            this.player = player;
            this.handDatabase = handDatabase;
            this.logger = logger;
        }

        public int GetExpectedChangeScore()
        {
            var establishedHands = handDatabase.SearchHands(player.GetPieceList());
            var score = establishedHands.Sum(hand => hand.Score * hand.GetNumberOfSuccesses(player.GetPieceList()));

            var increasedDifference = establishedHands?.Except(previousHands ?? new IHand[] { })?.ToArray() ?? establishedHands;
            var decreasedDifference = previousHands?.Except(establishedHands)?.ToArray() ?? new IHand[]{ };
            
            //ログ出力の移動
            foreach (IHand hand in increasedDifference)
                logger.Log("役 " + hand.Name + "が成立しました.");
            foreach (IHand hand in decreasedDifference)
                logger.Log("役 " + hand.Name + "が不成立になりました.");
            previousHands = establishedHands;

            return score;
        }
    }
}