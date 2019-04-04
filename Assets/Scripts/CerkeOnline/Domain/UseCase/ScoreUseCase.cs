using System.Linq;
using UnityEngine;
using Azarashi.CerkeOnline.Domain.Entities;

namespace Azarashi.CerkeOnline.Domain.UseCase
{
    /// <summary>
    /// Playerの手駒を参照し, その時終季した場合に得られる得点を計算する.
    /// </summary>
    public class ScoreUseCase : IScoreUseCase
    {
        IHand[] previousPaltauilHands;
        public int Score => scoreHolder.GetScore(player);
        readonly IScoreHolder scoreHolder;
        readonly IPlayer player;

        public ScoreUseCase(IPlayer player, IScoreHolder scoreHolder)
        {
            this.player = player;
            this.scoreHolder = scoreHolder;
        }
    }
}