using Azarashi.CerkeOnline.Domain.Entities;

namespace Azarashi.CerkeOnline.Application.Language
{
    public static class FirstOrSecondTranslator
    {
        public static string Translate(Terminologies.FirstOrSecond season, ILanguageTranslator translator)
        {
            var keyString = season.ToString();
            return KeyStringTranslator.Translate(keyString, translator);
        }
    }
}