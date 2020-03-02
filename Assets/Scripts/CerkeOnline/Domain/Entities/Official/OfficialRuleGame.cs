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
        public int ScoreRate { get; private set; } = 1;
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
        public IObservable<Unit> OnSeasonEnd => onSeasonEnd;
        readonly Subject<Unit> onSeasonEnd = new Subject<Unit>();

        public IObservable<Unit> OnGameEnd => gameEndSubject;
        readonly Subject<Unit> gameEndSubject = new Subject<Unit>();

        readonly HandChangeObserver handChangeObserver;
        readonly SeaonSequencer seasonSequencer;

        public OfficialRuleGame(Encampment firstPlayerEncampment, IReadOnlyServiceLocator serviceLocator)
        {
            FirstPlayer = new Player(firstPlayerEncampment);
            SecondPlayer = new Player(Terminologies.GetReversal(firstPlayerEncampment));

            var frontPlayer = GetPlayer(Encampment.Front);
            var backPlayer = GetPlayer(Encampment.Back);
            StartNewSeason();
            ScoreHolder = new DefaultScoreHolder(new Dictionary<IPlayer, int> { { frontPlayer, 20 }, { backPlayer, 20 } });

            //終了条件をまとめる
            ScoreHolder.GetScore(frontPlayer).Where(value => value == 0).Subscribe(_ => gameEndSubject.OnNext(Unit.Default));
            ScoreHolder.GetScore(backPlayer).Where(value => value == 0).Subscribe(_ => gameEndSubject.OnNext(Unit.Default));

            //seasonSequencer.OnEndは季の開始の呼び出しと一体化している。
            //OnSeasonEndは季の開始前に呼び出されることが保証されている。

            handChangeObserver = new HandChangeObserver(HandDatabase, OnTurnEnd);
            seasonSequencer = new SeaonSequencer(handChangeObserver.Observable, serviceLocator.GetInstance<ISeasonDeclarationProvider>());
            seasonSequencer.OnEnd.Where(_ => seasonSequencer.CurrentSeason != null)
                .Subscribe(_ => { onSeasonEnd.OnNext(Unit.Default); StartNewSeason(); });
            seasonSequencer.OnEnd.Where(_ => seasonSequencer.CurrentSeason == null)
                .Subscribe(_ => gameEndSubject.OnNext(Unit.Default));
            seasonSequencer.OnContinue.Subscribe(_ => ScoreRate *= 2);  //専用のクラス内に隠ぺいすべきかも

            var scoreCalculator = new ScoreCalculator(HandDatabase);
            OnSeasonEnd.Select(_ => Terminologies.GetReversal(CurrentTurn)) //終季の時点で終季した人のターンが終わってしまっているのでこの形にしている。
                .Select(GetPlayer)                                          //終季の時点ではターンが終わらないようにした方がよい？
                .Select(scoreCalculator.Calculate)
                .Select(tuple => { tuple.score *= 2; return tuple; })
                .Subscribe(tuple => ScoreHolder.MoveScore(tuple.scorer, tuple.score));
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
            CurrentTurn = FirstOrSecond.First;

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