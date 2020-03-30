using System.Collections.Generic;
using System.IO;
using System.Linq;
using Utf8Json;

namespace Azarashi.CerkeOnline.Application.Language
{
    public class LanguageDataReader
    {
        readonly LanguageDictionaryFactory factory;

        private static class LanguageFilePath
        {
            readonly static string childDirectoryPath = @"/Languages";
            readonly static string childFilePath = @"/words.json";

            public static string DirectoryPath { get { return UnityEngine.Application.dataPath + childDirectoryPath; } }
            public static string GetFilePath(string directory) => directory + childFilePath;
        }

        public LanguageDataReader(LanguageDictionaryFactory factory)
        {
            this.factory = factory;
        }
        
        public ILanguageDictionary Read(string languageName)
        {
            var dictionary = (Dictionary<string, string>)JsonSerializer.Deserialize<dynamic>(LanguageFilePath.GetFilePath(languageName));
            return factory.Create(dictionary);
        }

        public IEnumerable<ILanguageDictionary> ReadAll()
        {
            string languageFolderPath = string.Empty;

            var directories = EnumerateDirectories();
            var dictionaries = directories.Where(directory => File.Exists(LanguageFilePath.GetFilePath(directory)))
                .Select(directory => (Dictionary<string, string>)JsonSerializer.Deserialize<dynamic>(LanguageFilePath.GetFilePath(directory)));
            return dictionaries.Select(dictionary => factory.Create(dictionary)).Where(x => x != null);
        }

        static IEnumerable<string> EnumerateDirectories()
            => Directory.EnumerateDirectories(LanguageFilePath.DirectoryPath);
    }
}