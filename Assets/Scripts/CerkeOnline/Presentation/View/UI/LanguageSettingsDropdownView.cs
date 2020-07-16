using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Dropdown;
using UniRx;
using Azarashi.CerkeOnline.Application.Language;

namespace Azarashi.CerkeOnline.Presentation.View
{
    public class LanguageSettingsDropdownView : MonoBehaviour
    {
        public IObservable<int> OnDropDownChanged => dropdown.OnValueChangedAsObservable().TakeUntilDestroy(this);

        [SerializeField] Dropdown dropdown = default;

        void Start()
        {
            if (dropdown == null) throw new NullReferenceException();

            var languages = LanguageManager.Instance.TranslatableLanguages.Where(data => !string.IsNullOrEmpty(data.Name));
            List<OptionData> options = languages.Select(data => new OptionData(data.Name)).ToList();
            dropdown.options = options;

            dropdown.value = options.Select(option => option.text)
                .Select((text, index) => (text, index))
                .FirstOrDefault(value => value.text == LanguageManager.Instance.Translator.Translate(TranslatableKeys.LanguageName))
                .index;
            dropdown.OnValueChangedAsObservable().Select(value => languages.Skip(value).First()).Subscribe(LanguageManager.Instance.SetLanguage);
        }
    }
}