using Azarashi.CerkeOnline.Domain.Entities;

namespace Azarashi.CerkeOnline.Application.Language
{
    public static class SeasonTranslator
    {
        public static string Translate(Terminologies.Season season, ILanguageTranslator translator)
        {
            var keyString = "Seasons" + season.ToString();
            return KeyStringTranslator.Translate(keyString, translator);
        }
    }
}