using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Azarashi.CerkeOnline.Application.Language
{
    public class LanguageManager : MonoBehaviour
    {
        static LanguageManager instance;
        public static LanguageManager Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<LanguageManager>();

                return instance;
            }
        }


        public IEnumerable<string> TranslatableKeys => translatableKeysObject.TranslatableKeys;
        public IObservable<ILanguageTranslator> OnLanguageChanged { get; } = new Subject<ILanguageTranslator>();

        ILanguageTranslator translator = default;
        public ILanguageTranslator Translator 
        {
            get
            {
                if(translator == null)
                    translator = LanguageTranlatorFactory.Create(languageSettingsObject.DefaultLanguageCode, TranslatableKeys);

                return translator;
            }
        }

        [SerializeField] TranslatableKeysObject translatableKeysObject = default;
        [SerializeField] LanguageSettingsObject languageSettingsObject = default;

        private void Start()
        {
        }
    }
}