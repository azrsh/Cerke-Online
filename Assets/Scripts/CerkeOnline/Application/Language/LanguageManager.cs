using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;

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
        public IObservable<ILanguageTranslator> OnLanguageChanged { get { return currentLanguageData.AsObservable().Select(data => data.Translator); } }

        readonly IReactiveProperty<LanguageData> currentLanguageData = new ReactiveProperty<LanguageData>();
        public ILanguageTranslator Translator 
        {
            get
            {
                if(currentLanguageData.Value.Translator == null)
                    currentLanguageData.Value = new LanguageData(languageSettingsObject.DefaultLanguageCode, LanguageTranlatorFactory.Create(languageSettingsObject.DefaultLanguageCode, TranslatableKeys));

                return currentLanguageData.Value.Translator;
            }
        }

        IEnumerable<LanguageData> translatableLanguages = default;
        public IEnumerable<LanguageData> TranslatableLanguages 
        {
            get
            {
                if (translatableLanguages == null)
                    translatableLanguages = LanguageTranlatorFactory.CreateAll(TranslatableKeys);

                return translatableLanguages;
            }
        }

        [SerializeField] TranslatableKeysObject translatableKeysObject = default;
        [SerializeField] LanguageSettingsObject languageSettingsObject = default;

        private void Awake()
        {
            if (LanguageManager.Instance != this)
                Destroy(this);

            currentLanguageData.Value = new LanguageData(languageSettingsObject.DefaultLanguageCode, LanguageTranlatorFactory.Create(languageSettingsObject.DefaultLanguageCode, TranslatableKeys));
        }

        public void SetLanguage(LanguageData data) => currentLanguageData.Value = data;
    }
}