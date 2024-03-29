﻿using System.Collections.Generic;
using System.Linq;

namespace Azarashi.CerkeOnline.Application.Language
{
    public interface ILanguageDictionary
    {
        string this[TranslatableKeys index] { get; }
    }
    
    public class LanguageDictionaryFactory
    {
        readonly IEnumerable<string> translatableKeys;

        public LanguageDictionaryFactory(IEnumerable<string> translatableKeys)
        {
            this.translatableKeys = translatableKeys;
        }

        public ILanguageDictionary Create(IReadOnlyDictionary<string, string> source)
        {
            //LanguageDictionaryのKeyとtranlatableNameCodesが一致することを保証する
            if (!VerifyDictionary(source)) return null;

            return new LanguageDictionary(source);
        }

        private bool VerifyDictionary(IReadOnlyDictionary<string, string> source)
        {
            var shortage = translatableKeys.Except(source.Keys);
            var excess = source.Keys.Except(translatableKeys);

            foreach (var item in excess)
            {
                UnityEngine.Debug.LogError(item + " is an extra element.");
            }

            foreach (var item in shortage)
            {
                UnityEngine.Debug.LogError(item + " is an missing element.");
            }

            return !excess.Any() && !shortage.Any();
        }

        /// <summary>
        /// TranslatableKeysが0 ~ Count - 1 であることが保証されている必要がある.
        /// </summary>
        private class LanguageDictionary : ILanguageDictionary
        {
            public string this[TranslatableKeys key] => dictionary[(int)key];

            readonly IReadOnlyList<string> dictionary;

            public LanguageDictionary(IReadOnlyDictionary<string, string> source)
            {
                var array = new string[source.Count];
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = source[((TranslatableKeys)i).ToString()];
                }

                dictionary = array;
            }
        }
    }
}