﻿using System;
using UniRx;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    public class HandChangeObserver
    {
        readonly IHandDatabase handDatabase;
        readonly HandDifferenceCalculator handDifferenceCalculator;

        public IObservable<IReadOnlyPlayer> Observable => subject;
        Subject<IReadOnlyPlayer> subject = new Subject<IReadOnlyPlayer>();

        public HandChangeObserver(IHandDatabase handDatabase, IObservable<IReadOnlyPlayer> onTurnEnd)
        {
            this.handDatabase = handDatabase;
            handDifferenceCalculator = new HandDifferenceCalculator();
            onTurnEnd.Subscribe(CheckHandDifference);
        }

        //前回の役の保存、これでいいのか？
        IHand[] previousHands = new IHand[0];
        void CheckHandDifference(IReadOnlyPlayer currentPlayer)
        {
            IHand[] currentHands = handDatabase.SearchHands(currentPlayer.GetPieceList());
            HandDifference handDifference = handDifferenceCalculator.Calculate(previousHands, currentHands);
            if (handDifference.IncreasedDifference.Length > 0)
                subject.OnNext(currentPlayer);
        }

        public void Reset() => previousHands = new IHand[0];
    }
}