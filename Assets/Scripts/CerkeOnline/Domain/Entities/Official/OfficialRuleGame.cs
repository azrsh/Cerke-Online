using System;
using System.Linq;
using System.Collections.Generic;
using UniRx;
using Azarashi.Utilities;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.Official
{
    public partial class OfficialRuleGame : IGame
    {
        public ISeason CurrentSeason => seasonSequencer.CurrentSeason;
        public IBoard Board { get; private set; }
        public IHandDatabase HandDatabase { get; private set; }
        public IScoreHolder ScoreHolder { get; }
        public FirstOrSecond CurrentTurn { get; private set; }
        public IPlayer FirstPlayer { get; }
        public IPlayer SecondPlayer { get; }

        public IObservable<Unit> OnTurnChanged => onTurnChanged;
        readonly Subject<Unit> onTurnChanged = new Subject<Unit>();

        public IObservable<IReadOnlyPlayer> OnTurnEnd => onTurnEnd;
        readonly Subject<IReadOnlyPlayer> onTurnEnd = new Subject<IReadOnlyPlayer>();

        public IPlayer CurrentPlayer => GetPlayer(CurrentTurn);

        public IObservable<Unit> OnSeasonStart => onSeasonStart;
        readonly Subject<Unit> onSeasonStart = new Subject<Unit>();

        readonly HandChangeObserver handChangeObserver;
        readonly SeaonSequencer seasonSequencer;

        public OfficialRuleGame(Encampment firstPlayerEncampment, IReadOnlyServiceLocator serviceLocator)
        {
            FirstPlayer = new Player(firstPlayerEncampment);
            SecondPlayer = new Player(Terminologies.GetReversal(firstPlayerEncampment));
            CurrentTurn = FirstOrSecond.First;
        
            var frontPlayer = GetPlayer(Encampment.Front);
            var backPlayer = GetPlayer(Encampment.Back);
            StartNewSeason();
            ScoreHolder = new DefaultScoreHolder(new Dictionary<IPlayer, int> { { frontPlayer, 20 }, { backPlayer, 20 } });


            handChangeObserver = new HandChangeObserver(HandDatabase, OnTurnEnd);
            seasonSequencer = new SeaonSequencer(handChangeObserver.Observable, serviceLocator.GetInstance<ISeasonDeclarationProvider>());
            seasonSequencer.OnEnd.Subscribe(_ => StartNewSeason());
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
            onTurnEnd.OnNext(CurrentPlayer);
            CurrentTurn = Terminologies.GetReversal(CurrentTurn);
            onTurnChanged.OnNext(Unit.Default);
        }

        void StartNewSeason()
        {
            var frontPlayer = GetPlayer(Encampment.Front);
            var backPlayer = GetPlayer(Encampment.Back);
            Board = BoardFactory.Create(frontPlayer, backPlayer); 
            HandDatabase = new HandDatabase(Board, OnTurnChanged);
            handChangeObserver?.Reset();

            //PickOut All
            var frontPlayerPieceListCache = frontPlayer.GetPieceList().ToArray();
            foreach (var item in frontPlayerPieceListCache)
                frontPlayer.PickOut(item as IPiece);
            //PickOut All
            var backPlayerPieceListCache = backPlayer.GetPieceList().ToArray();
            foreach (var item in backPlayerPieceListCache)
                backPlayer.PickOut(item as IPiece);

            //onTurnEnd.OnNext(CurrentPlayer); 
            onTurnChanged.OnNext(Unit.Default);
            onSeasonStart.OnNext(Unit.Default);
        }
    }
}