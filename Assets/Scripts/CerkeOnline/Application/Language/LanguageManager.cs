using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;
using TMPro;

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
                if (currentLanguageData.Value.Translator == null)
                    currentLanguageData.Value = GetDefaultLanguageData();

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

            currentLanguageData.Value = GetDefaultLanguageData();
        }

        public void SetLanguage(LanguageData data) => currentLanguageData.Value = data;

        LanguageData GetDefaultLanguageData()
        {
            return new LanguageData(
                languageSettingsObject.DefaultLanguageCode, 
                LanguageTranlatorFactory.Create(
                    languageSettingsObject.DefaultLanguageCode, 
                    TranslatableKeys
                )
            );
        }
    }
}