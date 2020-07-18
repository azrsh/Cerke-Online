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
    public class EncampmentSelectionView : MonoBehaviour
    {
        public IObservable<int> OnDropDownChanged => dropdown.onValueChanged.AsObservable().TakeUntilDestroy(this);

        [SerializeField] TMP_Dropdown dropdown = default;

        void Start()
        {
            Assert.IsNotNull(dropdown);

            List<OptionData> options = Enum.GetNames(typeof(Encampment))
                .Select(name => "Encampment" + name)
                .Select(name => (TranslatableKeys)Enum.Parse(typeof(TranslatableKeys), name))
                .Select(key => LanguageManager.Instance.Translator.Translate(key))
                .Select(data => new OptionData(data.Text)).ToList();
            dropdown.options = options;

            //選択肢ごとに個別にしたい
            var font = LanguageManager.Instance.Translator.Translate(TranslatableKeys.EncampmentDropdownLabel).FontAsset;
            dropdown.captionText.font = font;
            dropdown.itemText.font = font;
        }

    }
}