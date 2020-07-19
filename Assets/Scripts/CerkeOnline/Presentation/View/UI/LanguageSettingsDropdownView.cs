using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UniRx;
using TMPro;
using static TMPro.TMP_Dropdown;
using Azarashi.CerkeOnline.Application.Language;

namespace Azarashi.CerkeOnline.Presentation.View
{
    public class LanguageSettingsDropdownView : MonoBehaviour
    {
        [SerializeField] TMP_Dropdown dropdown = default;

        void Start()
        {
            Assert.IsNotNull(dropdown);

            var languages = LanguageManager.Instance.TranslatableLanguages.Where(data => !string.IsNullOrEmpty(data.Name));
            List<OptionData> options = languages.Select(data => new OptionData(data.Name)).ToList();
            dropdown.options = options;

            TextData textData = LanguageManager.Instance.Translator.Translate(TranslatableKeys.LanguageName);
            dropdown.value = options.Select(option => option.text)
                .Select((text, index) => (text, index))
                .FirstOrDefault(value => value.text == textData.Text)
                .index;
            dropdown.captionText.font = textData.FontAsset;
            dropdown.itemText.font = textData.FontAsset;    //複数のフォントに対応できていない
            dropdown.onValueChanged.AsObservable().TakeUntilDestroy(this).Select(value => languages.Skip(value).First()).Subscribe(data =>
                {
                    var fontAsset = data.Translator.Translate(TranslatableKeys.LanguageName).FontAsset;
                    dropdown.captionText.font = fontAsset;
                    dropdown.itemText.font = fontAsset;    //複数のフォントに対応できていない
                    LanguageManager.Instance.SetLanguage(data); 
                });
        }
    }
}