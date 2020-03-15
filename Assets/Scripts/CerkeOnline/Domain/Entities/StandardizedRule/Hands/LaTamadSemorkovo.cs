using System.Collections.Generic;
using UniRx;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.Hands
{
    /// <summary>
    /// 撃皇
    /// </summary>
    public class LaTamadSemorkovo : IHand
    {
        public string Name { get; }
        public int Score { get; }

        int numberOfSurmounted = 0;

        public LaTamadSemorkovo(int score, ISteppedObservable steppedObservable)
        {
            Name = HandNameDictionary.PascalToJapanese[GetType().Name];
            Score = score;
            steppedObservable.OnStepped.Subscribe(_ => numberOfSurmounted++);
        }

        public int GetNumberOfSuccesses(IReadOnlyList<IReadOnlyPiece> pieces)
        {
            return numberOfSurmounted;
        }
    }
}