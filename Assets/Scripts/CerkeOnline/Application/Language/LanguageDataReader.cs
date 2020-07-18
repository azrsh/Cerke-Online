using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Utf8Json;
using TMPro;

namespace Azarashi.CerkeOnline.Application.Language
{
    public class LanguageDataReader
    {
        readonly LanguageDictionaryFactory factory;

        private static class LanguageFilePath
        {
            readonly static string childDirectoryPath = @"/Languages";
            readonly static string wordsJsonFilePath = @"/words.json";
            public static string LanguageRootDirectoryPath { get { return UnityEngine.Application.dataPath + childDirectoryPath; } }
            public static string LanguageDirectoryPath(string languageCode) => LanguageRootDirectoryPath + "/" + languageCode;
            public static string GetWordsJsonFilePath(string languageCode) => LanguageDirectoryPath(languageCode) + wordsJsonFilePath;
        }

        public LanguageDataReader(LanguageDictionaryFactory factory)
        {
            this.factory = factory;
        }
        
        public (ILanguageDictionary dictionary, TMP_FontAsset fontAsset) Read(string code)
        {  
            return (ReadDictionary(code), ReadFont(code));
        }

        public IEnumerable<(string code, ILanguageDictionary dictionary, TMP_FontAsset fontAsset)> ReadAll()
        {
            var names = Directory.EnumerateDirectories(LanguageFilePath.LanguageRootDirectoryPath).Select(Path.GetFileName);    //GetFileNameで末端のディレクトリ名を取得している.
            return names.Where(name => File.Exists(LanguageFilePath.GetWordsJsonFilePath(name)))
                .Select(name => (code : name, dictionary : ReadDictionary(name), fontAsset : ReadFont(name)))
                .Where(dictionary => dictionary.dictionary != null);
        }

        ILanguageDictionary ReadDictionary(string languageCode)
        {
            string json = string.Empty;
            using (StreamReader streamReader = new StreamReader(LanguageFilePath.GetWordsJsonFilePath(languageCode)))
            {
                json = streamReader.ReadToEnd();
            }

            var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            return factory.Create(dictionary);
        }

        TMP_FontAsset ReadFont(string code)
        {
            string languageDirectoryPath = LanguageFilePath.LanguageDirectoryPath(code);
            var fontFiles = Directory.EnumerateFiles(languageDirectoryPath, "*.otf").Union(Directory.EnumerateFiles(languageDirectoryPath, "*.ttf")).Union(Directory.EnumerateFiles(languageDirectoryPath, "*.ttc"));
            var fontFile = fontFiles.FirstOrDefault();
            if (string.IsNullOrEmpty(fontFile))
                return null;
            
            Font font = new Font(fontFile);
            TMP_FontAsset fontAsset = TMP_FontAsset.CreateFontAsset(font);
            return fontAsset;
        }
    }
}