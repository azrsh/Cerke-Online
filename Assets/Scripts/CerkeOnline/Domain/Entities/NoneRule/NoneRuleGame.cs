using System;
using UniRx;

namespace Azarashi.CerkeOnline.Domain.Entities.NoneRule
{
    public class NoneRuleGame : IGame
    {
        public IBoard Board => board;
        readonly IBoard board;

        public Terminologies.FirstOrSecond CurrentTurn { get; private set; }

        public IPlayer FirstPlayer => firstPlayer;
        readonly IPlayer firstPlayer = new Player();
        public IPlayer SecondPlayer => secondPlayer;
        readonly IPlayer secondPlayer = new Player();

        public IObservable<Unit> OnTurnChanged => onTurnChanged;
        readonly Subject<Unit> onTurnChanged = new Subject<Unit>();

        public IPlayer CurrentPlayer
        {
            get
            {
                return GetPlayer(CurrentTurn);
            }
        }

        public NoneRuleGame()
        {
            CurrentTurn = Terminologies.FirstOrSecond.First;
            board = new Board(FirstPlayer, SecondPlayer);
        }

        public IPlayer GetPlayer(Terminologies.FirstOrSecond firstOrSecond)
        {
            switch (firstOrSecond)
            {
            case Terminologies.FirstOrSecond.First:
                return firstPlayer;
            case Terminologies.FirstOrSecond.Second:
                return secondPlayer;
            default:
                return null;
            }
        }

        public void OnTurnEnd()
        {
            switch (CurrentTurn)
            {
            case Terminologies.FirstOrSecond.First:
                CurrentTurn = Terminologies.FirstOrSecond.Second;
                break;
            case Terminologies.FirstOrSecond.Second:
                CurrentTurn = Terminologies.FirstOrSecond.First;
                break;
            }

            onTurnChanged.OnNext(Unit.Default);
        }
    }
}