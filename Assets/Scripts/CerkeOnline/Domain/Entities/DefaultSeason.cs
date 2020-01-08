using System;
using UniRx;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    public class DefaultSeason : ISeason
    {
        public string Name { get; }
        public Terminologies.Season Season { get; }

        readonly Subject<Unit> continueSubject = new Subject<Unit>();
        public IObservable<Unit> OnContinue => continueSubject;

        readonly Subject<Unit> quitSubject = new Subject<Unit>();
        public IObservable<Unit> OnQuit => quitSubject;

        public DefaultSeason(Terminologies.Season season)
        {
            Name = Terminologies.SeasonDictionary.EnumToJapanese[season];
            Season = season;
        }

        public void Continue() => continueSubject.OnNext(Unit.Default);

        public void Quit() => quitSubject.OnNext(Unit.Default);
    }

    public enum SeasonContinueOrEnd
    {
        Continue, End
    };

    public interface ISeasonDeclarationProvider : IValueInputProvider<SeasonContinueOrEnd> { }
}