
namespace Azarashi.CerkeOnline.Domain.Entities
{
    /// <summary>
    /// 季の一回性を保証するオブジェクト. 同名の季でも別のオブジェクトになる.
    /// </summary>
    public class DefaultSeason : ISeason
    {
        public string Name { get; }
        public Terminologies.Season Season { get; }

        public DefaultSeason(Terminologies.Season season)
        {
            Name = Terminologies.SeasonDictionary.EnumToJapanese[season];
            Season = season;
        }
    }

    public enum SeasonContinueOrEnd
    {
        Continue, End
    };

    public interface ISeasonDeclarationProvider : IValueInputProvider<SeasonContinueOrEnd> { }
}