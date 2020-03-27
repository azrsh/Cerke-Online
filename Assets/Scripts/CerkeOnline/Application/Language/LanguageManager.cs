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


        public IReadOnlyList<string> TranslatableKeys => translatableKeysObject.TranslatableKeys;
        public IObservable<ILanguageTranslator> OnLanguageChanged { get; } = new Subject<ILanguageTranslator>();

        [SerializeField] public TranslatableKeysObject translatableKeysObject;

        private void Start()
        {
        }
    }
}