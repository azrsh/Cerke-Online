using System.Collections.Generic;
using System.Diagnostics;
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
            public static string GetFilePath(string languageName) => DirectoryPath + "/" + languageName + childFilePath;
        }

        public LanguageDataReader(LanguageDictionaryFactory factory)
        {
            this.factory = factory;
        }
        
        public ILanguageDictionary Read(string languageCode)
        {
            string json = string.Empty;
            using (StreamReader streamReader = new StreamReader(LanguageFilePath.GetFilePath(languageCode)))
            {
                json = streamReader.ReadToEnd();
            }

            var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            return factory.Create(dictionary);
        }

        public IEnumerable<(string code, ILanguageDictionary dictionary)> ReadAll()
        {
            var names = Directory.EnumerateDirectories(LanguageFilePath.DirectoryPath).Select(Path.GetFileName);    //GetFileNameで末端のディレクトリ名を取得している.
            return names.Where(name => File.Exists(LanguageFilePath.GetFilePath(name)))
                .Select(name => (code : name, dictionary : Read(name)))
                .Where(dictionary => dictionary.dictionary != null);
        }
    }
}