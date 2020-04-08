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

            //スマホ対応にあたって変更する必要がある
            public static string DirectoryPath { get { return UnityEngine.Application.dataPath + childDirectoryPath; } }
            public static string GetFilePath(string languageName) => DirectoryPath + "/" + languageName + childFilePath;
        }

        public LanguageDataReader(LanguageDictionaryFactory factory)
        {
            this.factory = factory;
        }
        
        public ILanguageDictionary Read(string languageName)
        {
            string json = string.Empty;
            using (StreamReader streamReader = new StreamReader(LanguageFilePath.GetFilePath(languageName)))
            {
                json = streamReader.ReadToEnd();
            }

            var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
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