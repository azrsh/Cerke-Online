using Azarashi.CerkeOnline.Domain.Entities;

namespace Azarashi.CerkeOnline.Domain.UseCase
{
    public class ScoreUseCase
    {
        readonly IPlayer player;
        readonly IHandDatabase handDatabase;

        public ScoreUseCase(IPlayer player, IHandDatabase handDatabase)
        {
            this.player = player;
            this.handDatabase = handDatabase;
        }

        public int GetScore()
        {
            int score = 0;
            foreach(IHand hand in handDatabase.SearchHands(player.GetPieceList()))
                score += hand.Score;
            return score;
        }
    }
}