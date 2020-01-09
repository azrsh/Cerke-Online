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
        public int Score => scoreHolder.GetScore(self);
        readonly IPlayer self;
        readonly IPlayer opponent;
        readonly IHandDatabase handDatabase;
        readonly IScoreHolder scoreHolder;
        readonly ILogger logger;

        public ScoreUseCase(IPlayer self, IPlayer opponent, IHandDatabase handDatabase, IScoreHolder scoreHolder, ILogger logger)
        {
            this.self = self;
            this.opponent = opponent;
            this.handDatabase = handDatabase;
            this.scoreHolder = scoreHolder;
            this.logger = logger;
        }

        public void MoveScore()
        {
            logger.Log("得点が移動します.");
            var selfScore = handDatabase.SearchHands(self.GetPieceList()).Sum(hand => hand.Score);
            var opponentScore = handDatabase.SearchHands(opponent.GetPieceList()).Sum(hand => hand.Score);
            var difference = selfScore - opponentScore;
            scoreHolder.MoveScore(self, opponent, difference /** ScoreRate*/);
        }
    }
}