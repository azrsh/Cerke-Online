using System;
using UniRx;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    public interface IGame
    {
        IBoard Board { get; }
        Terminologies.FirstOrSecond CurrentTurn { get; }
        IPlayer FirstPlayer { get; }
        IPlayer SecondPlayer { get; }
        IPlayer CurrentPlayer { get; }
        IPlayer GetPlayer(Terminologies.FirstOrSecond firstOrSecond);
        IObservable<Unit> OnTurnChanged { get; }
        void OnTurnEnd();
    }
}