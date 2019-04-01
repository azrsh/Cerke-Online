using System.Linq;
using UnityEngine;
using Azarashi.CerkeOnline.Domain.Entities;

namespace Azarashi.CerkeOnline.Domain.UseCase
{
    public class ScoreUseCase : IScoreUseCase
    {
        readonly IPlayer player;
        readonly IHandDatabase handDatabase;
        readonly ILogger logger;

        IHand[] previousHands;

        public ScoreUseCase(IPlayer player, IHandDatabase handDatabase, ILogger logger)
        {
            this.player = player;
            this.handDatabase = handDatabase;
            this.logger = logger;
        }

        public int GetScore()
        {
            var establishedHands = handDatabase.SearchHands(player.GetPieceList());
            var score = establishedHands.Sum(hand => hand.Score * hand.GetNumberOfSuccesses(player.GetPieceList()));

            var increasedDifference = establishedHands?.Except(previousHands ?? new IHand[] { })?.ToArray() ?? establishedHands;
            var decreasedDifference = previousHands?.Except(establishedHands)?.ToArray() ?? new IHand[]{ };
            foreach (IHand hand in increasedDifference)
                logger.Log("役 " + hand.Name + "が成立しました.");
            foreach (IHand hand in decreasedDifference)
                logger.Log("役 " + hand.Name + "が不成立になりました.");
            previousHands = establishedHands;

            return score;
        }
    }
}