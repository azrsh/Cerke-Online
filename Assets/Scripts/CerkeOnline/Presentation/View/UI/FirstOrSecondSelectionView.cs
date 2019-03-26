using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using static UnityEngine.UI.Dropdown;

namespace Azarashi.CerkeOnline.Presentation.View.UI
{
    public class FirstOrSecondSelectionView : MonoBehaviour
    {
        public IObservable<int> OnDropDownChanged => dropdown.OnValueChangedAsObservable().TakeUntilDestroy(this);

        [SerializeField] Dropdown dropdown = default;

        enum FirstOrSecondSelectionElements
        {
            FirstMove,SecondMove,Random
        }

        void Start()
        {
            if(dropdown == null) throw new NullReferenceException();

            List<OptionData> options = Enum.GetNames(typeof(FirstOrSecondSelectionElements)).Select(name => new OptionData(name)).ToList();
            dropdown.options = options;
        }
    }
}