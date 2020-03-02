using System;
using UniRx;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    public interface IGame
    {
        ISeason CurrentSeason { get; }
        IBoard Board { get; }
        IHandDatabase HandDatabase { get; }
        IScoreHolder ScoreHolder { get; }
        Terminologies.FirstOrSecond CurrentTurn { get; }
        IPlayer FirstPlayer { get; }
        IPlayer SecondPlayer { get; }
        IPlayer CurrentPlayer { get; }
        IPlayer GetPlayer(Terminologies.FirstOrSecond firstOrSecond);
        IPlayer GetPlayer(Terminologies.Encampment encampment);
        IObservable<Unit> OnTurnChanged { get; }
        IObservable<Unit> OnSeasonStart { get; }
        IObservable<Unit> OnSeasonEnd { get; }
        IObservable<Unit> OnGameEnd { get; }
        void TurnEnd();
    }
}