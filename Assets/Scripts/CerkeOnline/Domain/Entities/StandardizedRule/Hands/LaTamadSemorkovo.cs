using System.Collections.Generic;
using UniRx;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.Hands
{
    /// <summary>
    /// 撃皇
    /// </summary>
    internal class LaTamadSemorkovo : IHand
    {
        public HandName Name { get; }
        public int Score { get; }

        int numberOfSurmounted = 0;

        public LaTamadSemorkovo(int score, ISteppedObservable steppedObservable)
        {
            Name = HandName.TheStepping;
            Score = score;
            steppedObservable.OnStepped.Subscribe(_ => numberOfSurmounted++);
        }

        public int GetNumberOfSuccesses(IEnumerable<IReadOnlyPiece> pieces)
        {
            return numberOfSurmounted;
        }
    }
}