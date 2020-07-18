using System.Collections.Generic;
using System.Linq;
using TMPro;

namespace Azarashi.CerkeOnline.Application.Language
{
    public interface ILanguageTranslator
    {
        TextData Translate(TranslatableKeys key);
    }

    public static class LanguageTranlatorFactory
    {
        public static ILanguageTranslator Create(string languageCode, IEnumerable<string> translatableKeys)
        {
            LanguageDictionaryFactory languageDictionaryFactory = new LanguageDictionaryFactory(translatableKeys);
            LanguageDataReader languageDataReader = new LanguageDataReader(languageDictionaryFactory);
            var pair = languageDataReader.Read(languageCode);

            if (pair.dictionary == null)
            {
                UnityEngine.Debug.LogError("Language translator generation failed");
                return new EmptyTranslator();
            }

            return new LanguageTranslator(pair.dictionary, pair.fontAsset);
        }

        public static IEnumerable<LanguageData> CreateAll(IEnumerable<string> translatableKeys)
        {
            LanguageDictionaryFactory languageDictionaryFactory = new LanguageDictionaryFactory(translatableKeys);
            LanguageDataReader languageDataReader = new LanguageDataReader(languageDictionaryFactory);
            return languageDataReader.ReadAll().Select(dictionary => new LanguageData(dictionary.code, new LanguageTranslator(dictionary.dictionary, dictionary.fontAsset)));
        }

        private class LanguageTranslator : ILanguageTranslator
        {
            readonly ILanguageDictionary dictionary;
            readonly TMP_FontAsset fontAsset;

            public LanguageTranslator(ILanguageDictionary dictionary, TMP_FontAsset fontAsset)
            {
                this.dictionary = dictionary;
                this.fontAsset = fontAsset;
            }
            public TextData Translate(TranslatableKeys key) => new TextData(dictionary[key], fontAsset);
        }

        private class EmptyTranslator : ILanguageTranslator
        {
            public TextData Translate(TranslatableKeys key) => new TextData("EMPTY", null);
        }
    }
}