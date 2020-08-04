using System.Collections.Generic;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.Hands
{
    /// <summary>
    /// 皇再来
    /// </summary>
    internal class TheFutileMove : IHand
    {
        public HandName Name { get; }
        public int Score { get; }

        readonly TamObserver tamObserver;

        public TheFutileMove(int score, TamObserver tamObserver)
        {
            Name = HandName.TheFutileMove;
            Score = score;

            this.tamObserver = tamObserver;
        }

        public int GetNumberOfSuccesses(IEnumerable<IReadOnlyPiece> pieces)
        {
            return tamObserver.GetNumberOfTheFutileMove();
        }
    }
}