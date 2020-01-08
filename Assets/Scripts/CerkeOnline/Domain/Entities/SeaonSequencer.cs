using System;
using UniRx;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    public class SeaonSequencer
    {
        public ISeason CurrentSeason { get; private set; }
        
        readonly Subject<Unit> continueSubject = new Subject<Unit>();
        public IObservable<Unit> OnContinue => continueSubject;

        readonly Subject<Unit> endSubject = new Subject<Unit>();
        public IObservable<Unit> OnEnd => endSubject;

        public SeaonSequencer(IObservable<IReadOnlyPlayer> onSeasonChangeable, ISeasonDeclarationProvider seasonDeclarationProvider)
        {
            CurrentSeason = new DefaultSeason(Terminologies.Season.Spring);

            onSeasonChangeable.Subscribe(_ =>
            {
                seasonDeclarationProvider.RequestValue(OnDeclarated);
            });

            OnEnd.Subscribe(NextSeason);
        }

        void OnDeclarated(SeasonContinueOrEnd continueOrEnd)
        {
            switch (continueOrEnd)
            {
                case SeasonContinueOrEnd.Continue:
                    continueSubject.OnNext(Unit.Default);
                    break;
                case SeasonContinueOrEnd.End:
                    endSubject.OnNext(Unit.Default);
                    break;
            }
        }

        void NextSeason(Unit unit)
        {
            Terminologies.Season current = CurrentSeason.Season;
            Terminologies.Season next = Terminologies.GetNextSeason(current);

            CurrentSeason = new DefaultSeason(next);
        }

    }
}