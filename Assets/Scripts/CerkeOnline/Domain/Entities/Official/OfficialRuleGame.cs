using System;
using System.Collections.Generic;
using UniRx;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.Official
{
    public class OfficialRuleGame : IGame
    {
        public IBoard Board { get; }
        public IHandDatabase HandDatabase { get; }
        public IScoreHolder ScoreHolder { get; }
        public FirstOrSecond CurrentTurn { get; private set; }
        public IPlayer FirstPlayer { get; }
        public IPlayer SecondPlayer { get; }

        public IObservable<Unit> OnTurnChanged => onTurnChanged;
        readonly Subject<Unit> onTurnChanged = new Subject<Unit>();

        public IPlayer CurrentPlayer => GetPlayer(CurrentTurn);

        public OfficialRuleGame(Encampment firstPlayerEncampment)
        {
            FirstPlayer = new Player(firstPlayerEncampment);
            SecondPlayer = new Player(Terminologies.GetReversal(firstPlayerEncampment));
            CurrentTurn = FirstOrSecond.First;
        
            var frontPlayer = GetPlayer(Encampment.Front);
            var backPlayer = GetPlayer(Encampment.Back);
            Board = BoardFactory.Create(frontPlayer, backPlayer);
            HandDatabase = new HandDatabase(Board, OnTurnChanged);
            ScoreHolder = new DefaultScoreHolder(new Dictionary<IPlayer, int> { { frontPlayer, 20 }, { backPlayer, 20 } });
        }

        public IPlayer GetPlayer(FirstOrSecond firstOrSecond)
        {
            switch (firstOrSecond)
            {
            case FirstOrSecond.First:
                return FirstPlayer;
            case FirstOrSecond.Second:
                return SecondPlayer;
            default:
                return null;
            }
        }

        public IPlayer GetPlayer(Encampment encampment)
        {
            if (FirstPlayer.Encampment == encampment) return FirstPlayer;
            if (SecondPlayer.Encampment == encampment) return SecondPlayer;
            return null;
        }

        public void OnTurnEnd()
        {
            CurrentTurn = Terminologies.GetReversal(CurrentTurn);
            onTurnChanged.OnNext(Unit.Default);
        }
    }
}