using Azarashi.CerkeOnline.Domain.Entities;

namespace Azarashi.CerkeOnline.Domain.UseCase
{
    /// <summary>
    /// Playerを参照し, 得点を返却する.
    /// </summary>
    public class ScoreUseCase : IScoreUseCase
    {
        public int Score => scoreHolder.GetScore(player);
        readonly IPlayer player;
        readonly IScoreHolder scoreHolder;

        public ScoreUseCase(IPlayer player, IScoreHolder scoreHolder)
        {
            this.player = player;
            this.scoreHolder = scoreHolder;
        }
    }
}