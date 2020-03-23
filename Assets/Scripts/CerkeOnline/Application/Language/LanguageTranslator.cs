using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Azarashi.CerkeOnline.Application.Language
{
    public class LanguageTranslator
    {
        IReadOnlyDictionary<string,string> dictionary = new Dictionary<string,string>();

        public string Translate(string nameCode)
        {
            
            dictionary.TryGetValue(nameCode, out string result);
            return result;
        }
    }
}