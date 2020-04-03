using Azarashi.CerkeOnline.Domain.Entities;

namespace Azarashi.CerkeOnline.Application.Language
{
    public static class HandNameTranslator
    {
        public static string Translate(Terminologies.HandName handName, ILanguageTranslator translator)
        {
            var keyString = "Hands" + handName.ToString();
            return KeyStringTranslator.Translate(keyString, translator);
        }
    }
}