
namespace Azarashi.CerkeOnline.Domain.Entities
{
    /// <summary>
    /// 季の一回性を保証するオブジェクト. 同名の季でも別のオブジェクトになる.
    /// </summary>
    internal class DefaultSeason : ISeason
    {
        public Terminologies.Season Season { get; }

        internal DefaultSeason(Terminologies.Season season)
        {
            Season = season;
        }
    }

    public enum SeasonContinueOrEnd
    {
        Continue, End
    };

    public interface ISeasonDeclarationProvider : IValueInputProvider<SeasonContinueOrEnd> { }
}