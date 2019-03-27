using System;
using UniRx;

namespace Azarashi.CerkeOnline.Domain.Entities.NoRule
{
    public class NoRuleGame : IGame
    {
        public IBoard Board { get; }
        public Terminologies.FirstOrSecond CurrentTurn { get; private set; }
        public IPlayer FirstPlayer { get; }
        public IPlayer SecondPlayer { get; }
        
        public IObservable<Unit> OnTurnChanged => onTurnChanged;
        readonly Subject<Unit> onTurnChanged = new Subject<Unit>();

        public IPlayer CurrentPlayer
        {
            get
            {
                return GetPlayer(CurrentTurn);
            }
        }
        
        public NoRuleGame(Terminologies.Encampment firstPlayerEncampment)
        {
            FirstPlayer = new Player(firstPlayerEncampment);
            SecondPlayer = new Player(Terminologies.GetReversal(firstPlayerEncampment));
            CurrentTurn = Terminologies.FirstOrSecond.First;

            var frontPlayer = GetPlayer(Terminologies.Encampment.Front);
            var backPlayer = GetPlayer(Terminologies.Encampment.Back);
            Board = new Board(frontPlayer, backPlayer);
        }

        public IPlayer GetPlayer(Terminologies.FirstOrSecond firstOrSecond)
        {
            switch (firstOrSecond)
            {
            case Terminologies.FirstOrSecond.First:
                return FirstPlayer;
            case Terminologies.FirstOrSecond.Second:
                return SecondPlayer;
            default:
                return null;
            }
        }

        public IPlayer GetPlayer(Terminologies.Encampment encampment)
        {
            if (FirstPlayer.Encampment == encampment) return FirstPlayer;
            if (SecondPlayer.Encampment == encampment) return SecondPlayer;
            return null;
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