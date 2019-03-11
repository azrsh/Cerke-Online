using System;
using UniRx;

namespace Azarashi.CerkeOnline.Domain.Entities.Official
{
    public class OfficialRuleGame : IGame
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

        public OfficialRuleGame()
        {
            CurrentTurn = Terminologies.FirstOrSecond.FirstMove;
            board = new Board(FirstPlayer, SecondPlayer);
        }

        public IPlayer GetPlayer(Terminologies.FirstOrSecond firstOrSecond)
        {
            switch (firstOrSecond)
            {
            case Terminologies.FirstOrSecond.FirstMove:
                return firstPlayer;
            case Terminologies.FirstOrSecond.SecondMove:
                return secondPlayer;
            default:
                return null;
            }
        }

        public void OnTurnEnd()
        {
            switch (CurrentTurn)
            {
            case Terminologies.FirstOrSecond.FirstMove:
                CurrentTurn = Terminologies.FirstOrSecond.SecondMove;
                break;
            case Terminologies.FirstOrSecond.SecondMove:
                CurrentTurn = Terminologies.FirstOrSecond.FirstMove;
                break;
            }

            onTurnChanged.OnNext(Unit.Default);
        }
    }
}