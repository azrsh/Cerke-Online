using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    internal class HandChangeObserver
    {
        readonly IHandDatabase handDatabase;
        
        internal IObservable<IReadOnlyPlayer> Observable => subject;
        Subject<IReadOnlyPlayer> subject = new Subject<IReadOnlyPlayer>();

        //前回の役の保存、これでいいのか？
        IDictionary<IReadOnlyPlayer, IEnumerable<IHand>> previousHandsDictionary = new Dictionary<IReadOnlyPlayer, IEnumerable<IHand>>();

        internal HandChangeObserver(IHandDatabase handDatabase, IObservable<IReadOnlyPlayer> onTurnEnd)
        {
            this.handDatabase = handDatabase;
            onTurnEnd.Where(CheckHandIncrease).Subscribe(subject);
        }

        bool CheckHandIncrease(IReadOnlyPlayer currentPlayer)
        {
            if(!previousHandsDictionary.ContainsKey(currentPlayer))
                previousHandsDictionary.Add(currentPlayer, Enumerable.Empty<IHand>());

            IEnumerable<IHand> currentHands = handDatabase.SearchHands(currentPlayer.GetPieceList());
            HandDifference handDifference = HandDifferenceCalculator.Calculate(previousHandsDictionary[currentPlayer], currentHands);
            
            if(handDifference.IncreasedDifference.Any() || handDifference.IncreasedDifference.Any())
                previousHandsDictionary[currentPlayer] = currentHands.ToArray();
            
            return handDifference.IncreasedDifference.Any();
        }

        internal void Reset() => previousHandsDictionary.Clear();
    }
}