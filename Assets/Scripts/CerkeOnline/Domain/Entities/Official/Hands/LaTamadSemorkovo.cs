using System.Collections.Generic;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.Official.Hands
{
    /// <summary>
    /// 撃皇
    /// </summary>
    public class LaTamadSemorkovo : IHand
    {
        public string Name { get; }
        public int Score { get; }

        public LaTamadSemorkovo(int score)
        {
            Name = GetType().Name;
            Score = score;
        }

        public int GetNumberOfSuccesses(IReadOnlyList<IReadOnlyPiece> pieces)
        {
            return 0;
        }
    }
}