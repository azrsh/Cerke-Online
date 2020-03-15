using System;
using UniRx;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    public interface ISeasonSequencer
    {
        ISeason CurrentSeason { get; }
        IObservable<ISeason> OnStart { get; }
        IObservable<ISeason> OnContinue { get; }
        IObservable<ISeason> OnEnd { get; }
        bool StartNextSeason();
    }

    public class SeasonSequencer : ISeasonSequencer
    {
        public ISeason CurrentSeason { get; private set; }

        readonly IObserver<ISeason> startObserver;
        public IObservable<ISeason> OnStart { get; }

        readonly IObserver<ISeason> continueObserver;
        public IObservable<ISeason> OnContinue { get; }

        readonly IObserver<ISeason> endObserver;
        public IObservable<ISeason> OnEnd { get; }

        ISeason previousSeason = null;

        public SeasonSequencer(IObservable<IReadOnlyPlayer> onSeasonChangeable, ISeasonDeclarationProvider seasonDeclarationProvider, int seasonMaximamNumber)
        {
            CurrentSeason = new DefaultSeason(Terminologies.Season.Spring);

            onSeasonChangeable.Subscribe(_ =>
            {
                seasonDeclarationProvider.RequestValue(OnDeclarated);
            });

            var startSubject = new Subject<ISeason>();
            startObserver = startSubject;
            OnStart = startSubject.Take(seasonMaximamNumber);

            var endSubject = new Subject<ISeason>();
            endObserver = endSubject;
            OnEnd = endSubject.Take(seasonMaximamNumber);

            var continueSubject = new Subject<ISeason>();
            continueObserver = continueSubject;
            OnContinue = continueSubject.TakeUntil(OnEnd.Skip(seasonMaximamNumber - 1));    //終季の最後の一回と同時に購読停止
        }

        void OnDeclarated(SeasonContinueOrEnd continueOrEnd)
        {
            switch (continueOrEnd)
            {
                case SeasonContinueOrEnd.Continue:
                    continueObserver.OnNext(CurrentSeason);
                    break;
                case SeasonContinueOrEnd.End:
                    endObserver.OnNext(CurrentSeason);
                    EndSeason();
                    break;
            }
        }

        public bool StartNextSeason()
        {
            if (CurrentSeason != null) return false;

            Terminologies.Season previous = previousSeason.Season;
            Terminologies.Season next = Terminologies.GetNextSeason(previous);

            bool isNone = next != Terminologies.Season.None;
            if (isNone)
            {
                CurrentSeason = new DefaultSeason(next);
                startObserver.OnNext(CurrentSeason);
            }

            return isNone;
        }

        void EndSeason()
        {
            previousSeason = CurrentSeason;
            CurrentSeason = null;
        }

    }
}