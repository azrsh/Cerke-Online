using System;
using UniRx;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    internal class HandChangeObserver
    {
        readonly IHandDatabase handDatabase;
        
        internal IObservable<IReadOnlyPlayer> Observable => subject;
        Subject<IReadOnlyPlayer> subject = new Subject<IReadOnlyPlayer>();

        internal HandChangeObserver(IHandDatabase handDatabase, IObservable<IReadOnlyPlayer> onTurnEnd)
        {
            this.handDatabase = handDatabase;
            onTurnEnd.Where(CheckHandIncrease).Subscribe(subject);
        }

        //前回の役の保存、これでいいのか？
        IHand[] previousHands = new IHand[0];
        bool CheckHandIncrease(IReadOnlyPlayer currentPlayer)
        {
            IHand[] currentHands = handDatabase.SearchHands(currentPlayer.GetPieceList());
            HandDifference handDifference = HandDifferenceCalculator.Calculate(previousHands, currentHands);
            return handDifference.IncreasedDifference.Length > 0;
        }

        internal void Reset() => previousHands = new IHand[0];
    }
}