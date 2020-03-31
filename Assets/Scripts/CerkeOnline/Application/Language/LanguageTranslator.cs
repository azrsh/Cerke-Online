using System.Collections.Generic;

namespace Azarashi.CerkeOnline.Application.Language
{
    public interface ILanguageTranslator
    {
        string Translate(TranslatableKeys key);
    }

    public static class LanguageTranlatorFactory
    {
        public static ILanguageTranslator Create(string languageCode,IEnumerable<string> TranslatableKeys)
        {
            LanguageDictionaryFactory languageDictionaryFactory = new LanguageDictionaryFactory(TranslatableKeys);
            LanguageDataReader languageDataReader = new LanguageDataReader(languageDictionaryFactory);
            return new LanguageTranslator(languageDataReader.Read(languageCode));
        }

        private class LanguageTranslator : ILanguageTranslator
        {
            readonly ILanguageDictionary dictionary;
            public LanguageTranslator(ILanguageDictionary dictionary) => this.dictionary = dictionary;
            public string Translate(TranslatableKeys key) => dictionary[key];
        }
    }
}