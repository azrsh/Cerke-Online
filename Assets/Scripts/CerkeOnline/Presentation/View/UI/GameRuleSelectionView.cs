using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UniRx;
using TMPro;
using static TMPro.TMP_Dropdown;
using Azarashi.CerkeOnline.Application.Language;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Presentation.View.UI
{
    public class GameRuleSelectionView : MonoBehaviour
    {
        public IObservable<int> OnDropDownChanged => dropdown.onValueChanged.AsObservable().TakeUntilDestroy(this);

        [SerializeField] TMP_Dropdown dropdown = default;
        
        void Start()
        {
            Assert.IsNotNull(dropdown);

            List<OptionData> options = Enum.GetNames(typeof(RulesetName))
                .Select(name => "Ruleset" + name)
                .Select(keyString => (TranslatableKeys)Enum.Parse(typeof(TranslatableKeys), keyString))
                .Select(LanguageManager.Instance.Translator.Translate)
                .Select(data => data.Text)
                .Select(name => new OptionData(name)).ToList();
            dropdown.options = options;

            //選択肢ごとに個別にしたい
            var font = LanguageManager.Instance.Translator.Translate(TranslatableKeys.RulesetDropdownLabel).FontAsset;
            dropdown.captionText.font = font;
            dropdown.itemText.font = font;
        }

    }
}