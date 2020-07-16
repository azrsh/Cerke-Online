using System;
using UniRx;
using UniRx.Async;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.NoRule
{
    public class NoRuleGame : IGame
    {
        public ISeasonSequencer SeasonSequencer { get; }
        public IBoard Board { get; }
        public IHandDatabase HandDatabase { get; } = null;
        public IScoreHolder ScoreHolder { get; } = null;
        public FirstOrSecond CurrentTurn { get; private set; }
        public IPlayer FirstPlayer { get; }
        public IPlayer SecondPlayer { get; }

        public IObservable<Unit> OnTurnChanged => onTurnChanged;
        readonly Subject<Unit> onTurnChanged = new Subject<Unit>();
        public IObservable<Unit> OnSeasonStart { get; } = new Subject<Unit>();
        public IObservable<Unit> OnSeasonEnd { get; } = new Subject<Unit>();
        public IObservable<Unit> OnGameEnd { get; } = new Subject<Unit>();

        public IPlayer CurrentPlayer => GetPlayer(CurrentTurn);


        class AnonymousSeasonDeclarationProvider : ISeasonDeclarationProvider
        {
            public UniTask<SeasonContinueOrEnd> RequestValue() => new UniTask<SeasonContinueOrEnd>(SeasonContinueOrEnd.End);
        }
        public NoRuleGame(Encampment firstPlayerEncampment)
        {
            SeasonSequencer = new SeasonSequencer(Observable.Empty<IReadOnlyPlayer>(), new AnonymousSeasonDeclarationProvider(), 4);

            FirstPlayer = new Player(firstPlayerEncampment);
            SecondPlayer = new Player(Terminologies.GetReversal(firstPlayerEncampment));
            CurrentTurn = FirstOrSecond.First;

            var frontPlayer = GetPlayer(Encampment.Front);
            var backPlayer = GetPlayer(Encampment.Back);
            Board = BoardFactory.Create(frontPlayer, backPlayer);
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

        public void TurnEnd()
        {
            CurrentTurn = Terminologies.GetReversal(CurrentTurn);
            onTurnChanged.OnNext(Unit.Default);
        }
    }
}