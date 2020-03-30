using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Azarashi.CerkeOnline.Application.Language
{
    public interface ILanguageDictionary : IReadOnlyDictionary<string, string> { }
    
    public class LanguageDictionaryFactory
    {
        readonly IEnumerable<string> tranlatableNameCodes;

        public LanguageDictionaryFactory(IEnumerable<string> tranlatableNameCodes)
        {
            this.tranlatableNameCodes = tranlatableNameCodes;
        }

        public ILanguageDictionary Create(IDictionary<string, string> source)
        {
            //LanguageDictionaryのKeyとtranlatableNameCodesが一致することを保証する
            if (!VerifyDictionary(source)) return null;

            return new LanguageDictionary(source);
        }

        private bool VerifyDictionary(IDictionary<string, string> source)
            => tranlatableNameCodes.Count() == source.Count && tranlatableNameCodes.All(code => source.ContainsKey(code));

        private class LanguageDictionary : ILanguageDictionary
        {
            public string this[string key] => dictionary[key];
            public IEnumerable<string> Keys => dictionary.Keys;
            public IEnumerable<string> Values => dictionary.Values;
            public int Count => dictionary.Count;
            public bool ContainsKey(string key) => dictionary.ContainsKey(key);
            public IEnumerator<KeyValuePair<string, string>> GetEnumerator() => dictionary.GetEnumerator();
            public bool TryGetValue(string key, out string value) => dictionary.TryGetValue(key, out value);
            IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)dictionary).GetEnumerator();

            readonly Dictionary<string, string> dictionary;

            public LanguageDictionary(IDictionary<string, string> source)
            {
                dictionary = new Dictionary<string, string>(source);
            }
        }
    }
}