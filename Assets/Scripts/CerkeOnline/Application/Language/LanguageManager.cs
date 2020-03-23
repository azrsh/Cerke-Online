using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using Utf8Json;

namespace Azarashi.CerkeOnline.Application.Language
{
    public class LanguageManager
    {
        public static IReadOnlyList<string> TranslatableNameCodes { get; }
        public static IObservable<Unit> OnLanguageChanged { get; }

        [SerializeField] public TranslatableNameCodesObject translatableNameCodesObject;

        IEnumerable<ILanguageDictionary> LoadAllLanguageFile()
        {
            string languageFolderPath = string.Empty;
            const string childPath = @"/words.json";
            var factory = new LanguageDictionaryFactory(translatableNameCodesObject.TranslatableNameCodes);

            var directories = System.IO.Directory.EnumerateDirectories(languageFolderPath);
            var dictionaries = directories.Where(directory => System.IO.File.Exists(directory + childPath))
                .Select(directory => (Dictionary<string, string>)JsonSerializer.Deserialize<dynamic>(directory + childPath));
            return dictionaries.Select(dictionary => factory.Create(dictionary)).Where(x => x != null);
        }

    }
}