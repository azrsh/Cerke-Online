using System.Linq;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule
{
    public class ScoreCalculator
    {
        readonly IHandDatabase handDatabase;

        public ScoreCalculator(IHandDatabase handDatabase)
        {
            this.handDatabase = handDatabase;
        }

        public (IPlayer scorer, int score) Calculate(IPlayer player)
        {
            var score = handDatabase.SearchHands(player.GetPieceList()).Sum(hand => hand.Score);
            return (player, score);
        }
    }
}