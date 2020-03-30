using System.Collections.Generic;

namespace Azarashi.CerkeOnline.Application.Language
{
    public interface ILanguageTranslator
    {
        string Translate(string textCode);
    }

    public class LanguageTranslator
    {
        readonly IReadOnlyDictionary<string,string> dictionary = new Dictionary<string,string>();

        public string Translate(string nameCode)
        {
            if (!dictionary.ContainsKey(nameCode)) return string.Empty;
            
            return dictionary[nameCode];
        }
    }
}