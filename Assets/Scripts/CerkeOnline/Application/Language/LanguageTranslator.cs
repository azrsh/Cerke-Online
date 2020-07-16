using System.Collections.Generic;
using System.Linq;

namespace Azarashi.CerkeOnline.Application.Language
{
    public interface ILanguageTranslator
    {
        string Translate(TranslatableKeys key);
    }

    public static class LanguageTranlatorFactory
    {
        public static ILanguageTranslator Create(string languageCode, IEnumerable<string> translatableKeys)
        {
            LanguageDictionaryFactory languageDictionaryFactory = new LanguageDictionaryFactory(translatableKeys);
            LanguageDataReader languageDataReader = new LanguageDataReader(languageDictionaryFactory);
            var dictionary = languageDataReader.Read(languageCode);

            if (dictionary == null)
            {
                UnityEngine.Debug.LogError("Language translator generation failed");
                return new EmptyTranslator();
            }

            return new LanguageTranslator(dictionary);
        }

        public static IEnumerable<LanguageData> CreateAll(IEnumerable<string> translatableKeys)
        {
            LanguageDictionaryFactory languageDictionaryFactory = new LanguageDictionaryFactory(translatableKeys);
            LanguageDataReader languageDataReader = new LanguageDataReader(languageDictionaryFactory);
            return languageDataReader.ReadAll().Select(dictionary => new LanguageData(dictionary.code, new LanguageTranslator(dictionary.dictionary)));
        }

        private class LanguageTranslator : ILanguageTranslator
        {
            readonly ILanguageDictionary dictionary;
            public LanguageTranslator(ILanguageDictionary dictionary) => this.dictionary = dictionary;
            public string Translate(TranslatableKeys key) => dictionary[key];
        }

        private class EmptyTranslator : ILanguageTranslator
        {
            public string Translate(TranslatableKeys key) => "EMPTY";
        }
    }
}