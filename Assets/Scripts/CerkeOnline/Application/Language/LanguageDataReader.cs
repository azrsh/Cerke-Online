using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Utf8Json;

namespace Azarashi.CerkeOnline.Application.Language
{
    public class LanguageDataReader
    {
        readonly LanguageDictionaryFactory factory;
        readonly static string languageDirectoryPath;
        readonly static string childPath = @"/words.json";

        public LanguageDataReader(LanguageDictionaryFactory factory)
        {
            this.factory = factory;
        }

        public IEnumerable<ILanguageDictionary> ReadAll()
        {
            string languageFolderPath = string.Empty;

            var directories = EnumerateDirectories();
            var dictionaries = directories.Where(directory => File.Exists(directory + childPath))
                .Select(directory => (Dictionary<string, string>)JsonSerializer.Deserialize<dynamic>(directory + childPath));
            return dictionaries.Select(dictionary => factory.Create(dictionary)).Where(x => x != null);
        }

        static IEnumerable<string> EnumerateDirectories()
        {
            return Directory.EnumerateDirectories(languageDirectoryPath);
        }
    }
}